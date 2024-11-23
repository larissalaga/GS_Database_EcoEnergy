DROP TABLE t_gs24_orcamento;
DROP SEQUENCE seq_t_gs24_orcamento;

DROP TABLE t_gs24_simulacao;
DROP SEQUENCE seq_t_gs24_simulacao;

DROP TABLE t_gs24_endereco;
DROP SEQUENCE seq_t_gs24_endereco;

DROP TABLE t_gs24_usuario;
DROP SEQUENCE seq_t_gs24_usuario;

DROP TABLE t_gs24_empresa;
DROP SEQUENCE seq_t_gs24_empresa;

SET SERVEROUTPUT ON;
--------------------------------------------------------------------------------------------------------------------------------
CREATE SEQUENCE seq_t_gs24_empresa;

CREATE TABLE t_gs24_empresa (
    id_empresa       NUMBER NOT NULL PRIMARY KEY,
    nr_cnpj          VARCHAR2(14 CHAR) NOT NULL,
    nm_empresa       VARCHAR2(100 CHAR) NOT NULL,
    nr_telefone      VARCHAR2(13 CHAR) NOT NULL,
    ds_especialidade VARCHAR2(200 CHAR) NOT NULL
);

--SELECT * FROM t_gs24_empresa;
--------------------------------------------------------------------------------------------------------------------------------
CREATE SEQUENCE seq_t_gs24_usuario;

CREATE TABLE t_gs24_usuario (
    id_usuario  NUMBER NOT NULL PRIMARY KEY,
    nm_usuario  VARCHAR2(70 CHAR) NOT NULL,
    ds_email    VARCHAR2(100 CHAR) NOT NULL,
    nr_telefone VARCHAR2(13 CHAR) NOT NULL,
    nr_cpf      VARCHAR2(11 CHAR) NOT NULL,
    id_empresa  NUMBER,
    CONSTRAINT fk_gs24_usuario_empresa  
    FOREIGN KEY (id_empresa) REFERENCES t_gs24_empresa(id_empresa)
);

--SELECT * FROM t_gs24_usuario;
--------------------------------------------------------------------------------------------------------------------------------
CREATE SEQUENCE seq_t_gs24_endereco;

CREATE TABLE t_gs24_endereco (
    id_endereco   NUMBER NOT NULL PRIMARY KEY,
    ds_cep        VARCHAR2(8 CHAR) NOT NULL,
    ds_logradouro VARCHAR2(100 CHAR) NOT NULL,
    nr_logradouro NUMBER NOT NULL,
    ds_bairro     VARCHAR2(100 CHAR) NOT NULL,
    ds_cidade     VARCHAR2(100 CHAR) NOT NULL,
    ds_estado     VARCHAR2(100 CHAR) NOT NULL,
    ds_pais       VARCHAR2(100 CHAR) NOT NULL,
    id_usuario    NUMBER,
    id_empresa    NUMBER,
    CONSTRAINT fk_gs24_endereco_usuario  
    FOREIGN KEY (id_usuario) REFERENCES t_gs24_usuario(id_usuario),
    CONSTRAINT fk_gs24_endereco_empresa  
    FOREIGN KEY (id_empresa) REFERENCES t_gs24_empresa(id_empresa)
);

--SELECT * FROM t_gs24_endereco;
--------------------------------------------------------------------------------------------------------------------------
CREATE SEQUENCE seq_t_gs24_simulacao;

CREATE TABLE t_gs24_simulacao (
    id_simulacao                   NUMBER NOT NULL PRIMARY KEY,
    nr_custo_estimado              NUMBER NOT NULL,
    nr_economia                    NUMBER NOT NULL,
    dt_simulacao                   DATE NOT NULL,
    nr_consumo_mensal              NUMBER NOT NULL,
    nr_area_placa                  NUMBER NOT NULL,
    nr_potencia_estimada           NUMBER NOT NULL,
    nr_producao_mensal             NUMBER NOT NULL,
    nr_tempo_retorno_investimento  NUMBER NOT NULL,
    ds_orcamento_solicitado        NUMBER(1) NOT NULL,
    id_usuario                     NUMBER NOT NULL,
    id_endereco                    NUMBER NOT NULL,
    CONSTRAINT fk_gs24_simulacao_usuario
    FOREIGN KEY (id_usuario) REFERENCES t_gs24_usuario(id_usuario),
    CONSTRAINT fk_gs24_simulacao_endereco
    FOREIGN KEY (id_endereco) REFERENCES t_gs24_endereco (id_endereco)
);

--SELECT * FROM t_gs24_simulacao;
--------------------------------------------------------------------------------------------------------------------------------
CREATE SEQUENCE seq_t_gs24_orcamento;

CREATE TABLE t_gs24_orcamento (
    id_orcamento       NUMBER NOT NULL PRIMARY KEY,
    nr_valor_proposto  NUMBER NOT NULL,
    ds_prazo           VARCHAR2(20 CHAR) NOT NULL,
    dt_orcamento       DATE NOT NULL,
    ds_servicos        VARCHAR2(200 CHAR) NOT NULL,
    id_simulacao       NUMBER NOT NULL,    
    id_empresa         NUMBER NOT NULL,
    CONSTRAINT fk_gs24_orcamento_empresa  
    FOREIGN KEY (id_empresa) REFERENCES t_gs24_empresa(id_empresa),
    CONSTRAINT fk_gs24_orcamento_simulacao  
    FOREIGN KEY (id_simulacao) REFERENCES t_gs24_simulacao(id_simulacao)
);

--SELECT * FROM t_gs24_orcamento;
--------------------------------------------------------------------------------------------------------------------------------
