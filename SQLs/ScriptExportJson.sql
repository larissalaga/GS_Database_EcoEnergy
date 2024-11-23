-- Criação da tabela para armazenar o JSON
CREATE TABLE t_gs24_json_output (
    id NUMBER PRIMARY KEY,
    json_data CLOB,
    export_timestamp TIMESTAMP DEFAULT SYSTIMESTAMP
);

-- Execução da Procedure
BEGIN
    obter_simulacoes_json;
END;
/

-- Consulta para obter o JSON
SELECT json_data FROM t_gs24_json_output;

-- Procedure para exportar as simulações em JSON
CREATE OR REPLACE PROCEDURE obter_simulacoes_json AS
    v_json_output CLOB;
    v_temp_json CLOB;
BEGIN
    -- Inicializa o CLOB temporário
    DBMS_LOB.CREATETEMPORARY(v_json_output, TRUE);
    DBMS_LOB.CREATETEMPORARY(v_temp_json, TRUE);

    DBMS_LOB.WRITEAPPEND(v_json_output, LENGTH('{"simulacoes": ['), '{"simulacoes": [');

    FOR s_rec IN (
        SELECT s.id_simulacao, s.nr_custo_estimado, s.nr_economia, s.dt_simulacao AS data_simulacao,
               s.nr_consumo_mensal, s.nr_area_placa, s.nr_potencia_estimada, s.nr_producao_mensal,
               s.nr_tempo_retorno_investimento, e.ds_cep, e.ds_logradouro, e.nr_logradouro, e.ds_bairro,
               e.ds_cidade, e.ds_estado, e.ds_pais,
               (SELECT MAX(o.nr_valor_proposto) FROM t_gs24_orcamento o WHERE o.id_simulacao = s.id_simulacao) AS orcamento_mais_caro,
               (SELECT MIN(o.nr_valor_proposto) FROM t_gs24_orcamento o WHERE o.id_simulacao = s.id_simulacao) AS orcamento_mais_barato,
               (SELECT AVG(o.nr_valor_proposto) FROM t_gs24_orcamento o WHERE o.id_simulacao = s.id_simulacao) AS orcamento_medio
        FROM t_gs24_simulacao s
        JOIN t_gs24_endereco e ON s.id_endereco = e.id_endereco
    ) LOOP
        -- Monta o JSON para cada simulação
        v_temp_json := '{"id_simulacao": ' || s_rec.id_simulacao ||
                       ', "custo_estimado": ' || NVL(TO_CHAR(s_rec.nr_custo_estimado, 'FM9999990.00'), '0.00') ||
                       ', "economia": ' || NVL(TO_CHAR(s_rec.nr_economia, 'FM9999990.00'), '0.00') ||
                       ', "data_simulacao": "' || s_rec.data_simulacao ||
                       '", "consumo_mensal": ' || NVL(TO_CHAR(s_rec.nr_consumo_mensal, 'FM9999990.00'), '0.00') ||
                       ', "area_placa": ' || NVL(TO_CHAR(s_rec.nr_area_placa, 'FM9999990.00'), '0.00') ||
                       ', "potencia_estimada": ' || NVL(TO_CHAR(s_rec.nr_potencia_estimada, 'FM9999990.00'), '0.00') ||
                       ', "producao_mensal": ' || NVL(TO_CHAR(s_rec.nr_producao_mensal, 'FM9999990.00'), '0.00') ||
                       ', "tempo_retorno_investimento": ' || NVL(TO_CHAR(s_rec.nr_tempo_retorno_investimento, 'FM9999990.00'), '0.00') ||
                       ', "orcamentos": [';

        FOR o_rec IN (
            SELECT o.id_orcamento, o.nr_valor_proposto, o.ds_prazo, o.dt_orcamento AS data_orcamento, o.ds_servicos
            FROM t_gs24_orcamento o
            WHERE o.id_simulacao = s_rec.id_simulacao
        ) LOOP
            v_temp_json := v_temp_json || '{"id_orcamento": ' || o_rec.id_orcamento ||
                           ', "valor_proposto": ' || NVL(TO_CHAR(o_rec.nr_valor_proposto, 'FM9999990.00'), '0.00') ||
                           ', "prazo": "' || o_rec.ds_prazo ||
                           '", "data_orcamento": "' || o_rec.data_orcamento ||
                           '", "servicos": "' || o_rec.ds_servicos || '"},';
        END LOOP;

        -- Remove a última vírgula, se necessário
        IF v_temp_json LIKE '%},' THEN
            v_temp_json := RTRIM(v_temp_json, ',');
        END IF;

        v_temp_json := v_temp_json || '], "orcamento_mais_caro": ' || NVL(TO_CHAR(s_rec.orcamento_mais_caro, 'FM9999990.00'), 'null') ||
                       ', "orcamento_mais_barato": ' || NVL(TO_CHAR(s_rec.orcamento_mais_barato, 'FM9999990.00'), 'null') ||
                       ', "orcamento_medio": ' || NVL(TO_CHAR(s_rec.orcamento_medio, 'FM9999990.00'), 'null') ||
                       ', "endereco": {"cep": "' || s_rec.ds_cep ||
                       '", "logradouro": "' || s_rec.ds_logradouro ||
                       '", "numero": ' || s_rec.nr_logradouro ||
                       ', "bairro": "' || s_rec.ds_bairro ||
                       '", "cidade": "' || s_rec.ds_cidade ||
                       '", "estado": "' || s_rec.ds_estado ||
                       '", "pais": "' || s_rec.ds_pais || '"}}';

        -- Adiciona a vírgula, se necessário
        IF DBMS_LOB.GETLENGTH(v_json_output) > LENGTH('{"simulacoes": [') THEN
            DBMS_LOB.WRITEAPPEND(v_json_output, LENGTH(','), ',');
        END IF;

        -- Adiciona o JSON da simulação ao CLOB principal
        DBMS_LOB.APPEND(v_json_output, v_temp_json);
    END LOOP;

    DBMS_LOB.WRITEAPPEND(v_json_output, LENGTH(']}'), ']}');

    -- Armazena o JSON em uma tabela para posterior consulta
    MERGE INTO t_gs24_json_output tgt
    USING (SELECT v_json_output AS json_data FROM dual) src
    ON (tgt.id = 1)
    WHEN MATCHED THEN
        UPDATE SET tgt.json_data = src.json_data, tgt.export_timestamp = SYSTIMESTAMP
    WHEN NOT MATCHED THEN
        INSERT (id, json_data, export_timestamp) VALUES (1, src.json_data, SYSTIMESTAMP);

    -- Libera o CLOB temporário
    DBMS_LOB.FREETEMPORARY(v_json_output);
    DBMS_LOB.FREETEMPORARY(v_temp_json);
END obter_simulacoes_json;

/
