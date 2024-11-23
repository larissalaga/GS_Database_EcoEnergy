--SET SERVEROUTPUT ON;

--------------------------------------------------------------------------------------------------------------------------------
-- INSERT_EMPRESA
SELECT *
FROM t_gs24_empresa
ORDER BY id_empresa;
--
DECLARE
BEGIN
    insert_empresa(
            '08986016000127',
            'SOL PRA TODOS',
            '31955487610',
            'Fornecimento e intalacao de placas solares'
    );
END;
/
--

CREATE OR REPLACE PROCEDURE insert_empresa(
    p_nr_cnpj VARCHAR2,
    p_nm_empresa VARCHAR2,
    p_nr_telefone VARCHAR2,
    p_ds_especialidade VARCHAR2
) IS
    invalid_cnpj EXCEPTION;
    empresa_exists EXCEPTION;
    telefone_invalid EXCEPTION;
    empresa_id NUMBER;
BEGIN
    -- Chamar a função para validar o CNPJ
    IF NOT fun_valida_cnpj(p_nr_cnpj) THEN
        RAISE invalid_cnpj;
    END IF;

    -- Busca o paciente
    BEGIN
        SELECT id_empresa
        INTO empresa_id
        FROM t_gs24_empresa
        WHERE nr_cnpj = p_nr_cnpj;
    EXCEPTION
        WHEN no_data_found THEN
            empresa_id := NULL;
    END;
    IF empresa_id IS NOT NULL THEN
        RAISE empresa_exists;
    END IF;

    -- Valida o telefone
    IF NOT fun_valida_telefone(p_nr_telefone) THEN
        RAISE telefone_invalid;
    END IF;

    -- Inserindo empresa
    INSERT INTO t_gs24_empresa
    (id_empresa,
     nr_cnpj,
     nm_empresa,
     nr_telefone,
     ds_especialidade)
    VALUES (seq_t_gs24_empresa.nextval,
            p_nr_cnpj,
            p_nm_empresa,
            p_nr_telefone,
            p_ds_especialidade);
    COMMIT;
    dbms_output.put_line('Empresa inserida com sucesso.');
EXCEPTION
    WHEN invalid_cnpj THEN
        RAISE_APPLICATION_ERROR(-20001, 'CNPJ Inválido.');
    WHEN empresa_exists THEN
        RAISE_APPLICATION_ERROR(-20002, 'CNPJ já cadastrado.');
    WHEN telefone_invalid THEN
        RAISE_APPLICATION_ERROR(-20006, 'Telefone inválido. Deve ter 11 digitos, com DDD sem 0, ex 11987654321');
    WHEN OTHERS THEN
        RAISE_APPLICATION_ERROR(-20000, 'ERRO DESCONHECIDO.' || SQLERRM);
END insert_empresa;
/

--------------------------------------------------------------------------------------------------------------------------------
-- INSERT_USUARIO
SELECT *
FROM t_gs24_usuario
ORDER BY id_usuario;
--
DECLARE
BEGIN
    insert_usuario(
            'Maria Josefina',
            'majao@gmail.br',
            '31955487610',
            '61285215095',
            1
    );
END;
--
CREATE OR REPLACE PROCEDURE insert_usuario(
    p_nm_usuario VARCHAR2,
    p_ds_email VARCHAR2,
    p_nr_telefone VARCHAR2,
    p_nr_cpf VARCHAR2,
    p_id_empresa NUMBER DEFAULT NULL,
    p_nr_cnpj VARCHAR2 DEFAULT NULL
) IS
    invalid_cpf EXCEPTION;
    usuario_exists EXCEPTION;
    empresa_not_found EXCEPTION;
    telefone_invalid EXCEPTION;
    email_invalid EXCEPTION;
    invalid_cnpj EXCEPTION;
    usuario_id NUMBER;
    empresa_id NUMBER;
BEGIN
    -- Validação de CPF
    IF NOT fun_valida_cpf(p_nr_cpf) THEN
        RAISE invalid_cpf;
    END IF;

    -- Busca o usuario
    BEGIN
        SELECT id_usuario
        INTO usuario_id
        FROM t_gs24_usuario
        WHERE nr_cpf = p_nr_cpf;
    EXCEPTION
        WHEN no_data_found THEN
            usuario_id := NULL;
    END;
    IF usuario_id IS NOT NULL THEN
        RAISE usuario_exists;
    END IF;

    IF p_id_empresa IS NOT NULL THEN
        -- Busca a empresa
        BEGIN
            SELECT id_empresa
            INTO empresa_id
            FROM t_gs24_empresa
            WHERE id_empresa = p_id_empresa;
        EXCEPTION
            WHEN no_data_found THEN
                RAISE empresa_not_found;
        END;
    ELSIF p_nr_cnpj IS NOT NULL THEN
        -- Validação de CNPJ
        IF NOT fun_valida_cnpj(p_nr_cnpj) THEN
            RAISE invalid_cnpj;
        END IF;

        -- Busca a empresa pelo CNPJ
        BEGIN
            SELECT id_empresa
            INTO empresa_id
            FROM t_gs24_empresa
            WHERE nr_cnpj = p_nr_cnpj;
        EXCEPTION
            WHEN no_data_found THEN
                RAISE empresa_not_found;
        END;
    ELSE
        empresa_id := NULL;
    END IF;

    -- Valida o email
    IF NOT fun_valida_email(p_ds_email) THEN
        RAISE email_invalid;
    END IF;

    -- Valida o telefone
    IF NOT fun_valida_telefone(p_nr_telefone) THEN
        RAISE telefone_invalid;
    END IF;

    -- Inserindo usuarios
    INSERT INTO t_gs24_usuario
    (id_usuario,
     nm_usuario,
     ds_email,
     nr_telefone,
     nr_cpf,
     id_empresa)
    VALUES (seq_t_gs24_usuario.nextval,
            p_nm_usuario,
            p_ds_email,
            p_nr_telefone,
            p_nr_cpf,
            p_id_empresa);
    COMMIT;
    dbms_output.put_line('Usuário inserido com sucesso.');
EXCEPTION
    WHEN invalid_cpf THEN
        RAISE_APPLICATION_ERROR(-20001, 'CPF Inválido.');
    WHEN usuario_exists THEN
        RAISE_APPLICATION_ERROR(-20002, 'CPF já cadastrado.');
    WHEN empresa_not_found THEN
        RAISE_APPLICATION_ERROR(-20003, 'Empresa não existe.');
    WHEN invalid_cnpj THEN
        RAISE_APPLICATION_ERROR(-20004, 'CNPJ Inválido.');
    WHEN email_invalid THEN
        RAISE_APPLICATION_ERROR(-20005,
                                'Email inválido. Email deve ter formato user@example.com, user@example.org, user@example.br');
    WHEN telefone_invalid THEN
        RAISE_APPLICATION_ERROR(-20006, 'Telefone inválido. Deve ter 11 digitos, com DDD sem 0, ex 11987654321');
    WHEN OTHERS THEN
        RAISE_APPLICATION_ERROR(-20000, 'ERRO DESCONHECIDO.' || SQLERRM);
END insert_usuario;
/

--------------------------------------------------------------------------------------------------------------------------------
-- insert_endereco
SELECT *
FROM t_gs24_endereco
ORDER BY id_endereco;
--
DECLARE
BEGIN
    insert_endereco(
            '02726010',
            'Ouro Fino',
            119,
            'Nova Gardenia',
            'São Paulo',
            'SP',
            'Brasil',
            1,
            1
    );
END;
--
CREATE OR REPLACE PROCEDURE insert_endereco(
    p_ds_cep VARCHAR2,
    p_ds_logradouro VARCHAR2,
    p_nr_logradouro NUMBER,
    p_ds_bairro VARCHAR2,
    p_ds_cidade VARCHAR2,
    p_ds_estado VARCHAR2,
    p_ds_pais VARCHAR2,
    p_d_usuario NUMBER,
    p_id_empresa NUMBER
) IS
    invalid_cep EXCEPTION;
    usuario_not_found EXCEPTION;
    empresa_not_found EXCEPTION;
    usuario_id NUMBER;
    empresa_id NUMBER;
BEGIN
    -- Validação de CEP
    IF NOT fun_valida_cep(p_ds_cep) THEN
        RAISE invalid_cep;
    END IF;

    IF p_d_usuario IS NOT NULL THEN
        -- Busca o usuario
        BEGIN
            SELECT id_usuario
            INTO usuario_id
            FROM t_gs24_usuario
            WHERE id_usuario = p_d_usuario;
        EXCEPTION
            WHEN no_data_found THEN
                RAISE usuario_not_found;
        END;
    END IF;

    IF p_id_empresa IS NOT NULL THEN
        -- Busca a empresa
        BEGIN
            SELECT id_empresa
            INTO empresa_id
            FROM t_gs24_empresa
            WHERE id_empresa = p_id_empresa;
        EXCEPTION
            WHEN no_data_found THEN
                RAISE empresa_not_found;
        END;
    END IF;

    -- Inserindo endereco
    INSERT INTO t_gs24_endereco
    (id_endereco,
     ds_cep,
     ds_logradouro,
     nr_logradouro,
     ds_bairro,
     ds_cidade,
     ds_estado,
     ds_pais,
     id_usuario,
     id_empresa)
    VALUES (seq_t_gs24_endereco.nextval,
            p_ds_cep,
            p_ds_logradouro,
            p_nr_logradouro,
            p_ds_bairro,
            p_ds_cidade,
            p_ds_estado,
            p_ds_pais,
            p_d_usuario,
            p_id_empresa);
    COMMIT;
    dbms_output.put_line('Endereço inserido com sucesso.');
EXCEPTION
    WHEN invalid_cep THEN
        RAISE_APPLICATION_ERROR(-20001, 'CEP Inválido.');
    WHEN usuario_not_found THEN
        RAISE_APPLICATION_ERROR(-20003, 'Usuário não existe.');
    WHEN empresa_not_found THEN
        RAISE_APPLICATION_ERROR(-20003, 'Empresa não existe.');
    WHEN OTHERS THEN
        RAISE_APPLICATION_ERROR(-20000, 'ERRO DESCONHECIDO.' || SQLERRM);
END insert_endereco;
/

--------------------------------------------------------------------------------------------------------------------------------
-- insert_simulacao
SELECT *
FROM t_gs24_simulacao
ORDER BY id_simulacao;
--
DECLARE
BEGIN
    insert_simulacao(
        20000,
        700,
        SYSDATE,
        400,
        60,
        6,
        700,
        5,
        1,
        '90610575074',
        12
    );
END;
/
--
CREATE OR REPLACE PROCEDURE insert_simulacao(
    p_nr_custo_estimado NUMBER,
    p_nr_economia NUMBER,
    p_dt_simulacao DATE,
    p_nr_consumo_mensal NUMBER,
    p_nr_area_placa NUMBER,
    p_nr_potencia_estimada NUMBER,
    p_nr_producao_mensal NUMBER,
    p_nr_tempo_retorno_investimento NUMBER,
    p_ds_orcamento_solicitado NUMBER,
    p_nr_cpf VARCHAR2,
    p_id_endereco NUMBER
) IS
    usuario_not_found EXCEPTION;
    endereco_not_found EXCEPTION;
    cpf_invalid EXCEPTION;
    usuario_id NUMBER;
    endereco_id NUMBER;
BEGIN
    -- Validação de CPF
    IF NOT fun_valida_cpf(p_nr_cpf) THEN
        RAISE cpf_invalid;
    END IF;

    -- Validação de Usuário
    BEGIN
        SELECT id_usuario
        INTO usuario_id
        FROM t_gs24_usuario
        WHERE nr_cpf = p_nr_cpf;
    EXCEPTION
        WHEN no_data_found THEN
            RAISE usuario_not_found;
    END;

    -- Validação se o Endereço pertence ao Usuário
    BEGIN
        SELECT id_endereco
        INTO endereco_id
        FROM t_gs24_endereco
        WHERE id_endereco = p_id_endereco AND id_usuario = usuario_id;
    EXCEPTION
        WHEN no_data_found THEN
            RAISE endereco_not_found;
    END;

    -- Inserindo simulação
    INSERT INTO t_gs24_simulacao(
        id_simulacao,
        nr_custo_estimado,
        nr_economia,
        dt_simulacao,
        nr_consumo_mensal,
        nr_area_placa,
        nr_potencia_estimada,
        nr_producao_mensal,
        nr_tempo_retorno_investimento,
        ds_orcamento_solicitado,
        id_usuario,
        id_endereco
    )
    VALUES (
        seq_t_gs24_simulacao.nextval,
        p_nr_custo_estimado,
        p_nr_economia,
        p_dt_simulacao,
        p_nr_consumo_mensal,
        p_nr_area_placa,
        p_nr_potencia_estimada,
        p_nr_producao_mensal,
        p_nr_tempo_retorno_investimento,
        p_ds_orcamento_solicitado,
        usuario_id,
        p_id_endereco
    );
    COMMIT;
    dbms_output.put_line('Simulação inserida com sucesso.');
EXCEPTION
    WHEN usuario_not_found THEN
        RAISE_APPLICATION_ERROR(-20003, 'Usuário não existe');
    WHEN endereco_not_found THEN
        RAISE_APPLICATION_ERROR(-20004, 'Endereço não existe ou não pertence ao usuário informado.');
    WHEN cpf_invalid THEN
        RAISE_APPLICATION_ERROR(-20001, 'CPF Inválido.');
    WHEN OTHERS THEN
        RAISE_APPLICATION_ERROR(-20000, 'ERRO DESCONHECIDO.' || SQLERRM);
END insert_simulacao;
/

--------------------------------------------------------------------------------------------------------------------------------
-- insert_orcamento
SELECT *
FROM t_gs24_orcamento
ORDER BY id_orcamento;
--
DECLARE
BEGIN
    insert_orcamento(
            9500,
            '2 meses',
            TO_DATE('07/01/2024'),
            'Colocação das placas e baterias',
            1,
            '96776703000185'
    );
END;

--
CREATE OR REPLACE PROCEDURE insert_orcamento(
    p_nr_valor_proposto NUMBER,
    p_ds_prazo VARCHAR2,
    p_dt_orcamento DATE,
    p_ds_servicos VARCHAR2,
    p_id_simulacao NUMBER,
    p_nr_cnpj VARCHAR2
) IS
    invalid_cnpj EXCEPTION;
    simulacao_not_found EXCEPTION;
    empresa_not_found EXCEPTION;
    simulacao_id NUMBER;
    empresa_id   NUMBER;
BEGIN
    -- Chamar a função para validar o CNPJ
    IF NOT fun_valida_cnpj(p_nr_cnpj) THEN
        RAISE invalid_cnpj;
    END IF;

    BEGIN
        SELECT id_empresa
        INTO empresa_id
        FROM t_gs24_empresa
        WHERE nr_cnpj = p_nr_cnpj;
    EXCEPTION
        WHEN no_data_found THEN
            RAISE empresa_not_found;
    END;

    BEGIN
        SELECT id_simulacao
        INTO simulacao_id
        FROM t_gs24_simulacao
        WHERE id_simulacao = p_id_simulacao;
    EXCEPTION
        WHEN no_data_found THEN
            RAISE simulacao_not_found;
    END;

    -- Inserindo orçamento
    INSERT INTO t_gs24_orcamento
    (id_orcamento,
     nr_valor_proposto,
     ds_prazo,
     dt_orcamento,
     ds_servicos,
     id_simulacao,
     id_empresa)
    VALUES (seq_t_gs24_orcamento.nextval,
            p_nr_valor_proposto,
            p_ds_prazo,
            p_dt_orcamento,
            p_ds_servicos,
            p_id_simulacao,
            empresa_id);
    COMMIT;
    dbms_output.put_line('Orçamento inserido com sucesso.');
EXCEPTION
    WHEN invalid_cnpj THEN
        RAISE_APPLICATION_ERROR(-20001, 'CNPJ Inválido.');
    WHEN simulacao_not_found THEN
        RAISE_APPLICATION_ERROR(-20003, 'Simulação não existe.');
    WHEN empresa_not_found THEN
        RAISE_APPLICATION_ERROR(-20003, 'Simulação não existe.');
    WHEN OTHERS THEN
        RAISE_APPLICATION_ERROR(-20000, 'ERRO DESCONHECIDO.' || SQLERRM);
END insert_orcamento;
/

--------------------------------------------------------------------------------------------------------------------------------
