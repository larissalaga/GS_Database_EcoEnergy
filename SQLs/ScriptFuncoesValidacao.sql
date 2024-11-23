SET SERVEROUTPUT ON;
--------------------------------------------------------------------------------------------------------------------------------
-- Validação de CPF: FUN_VALIDA_CPF
DECLARE
    CPF_NUMBER VARCHAR2(11) := '15288546320';
    IS_VALID   BOOLEAN;
BEGIN
    IS_VALID := FUN_VALIDA_CPF(CPF_NUMBER);
    IF IS_VALID THEN
        DBMS_OUTPUT.PUT_LINE('CPF is valid.');
    ELSE
        DBMS_OUTPUT.PUT_LINE('CPF is invalid.');
    END IF;
END;
/
--
CREATE OR REPLACE FUNCTION FUN_VALIDA_CPF(
    CPF VARCHAR
) RETURN BOOLEAN IS
    SUM1   NUMBER := 0;
    SUM2   NUMBER := 0;
    DIGIT1 NUMBER;
    DIGIT2 NUMBER;
    I      NUMBER;
BEGIN
    -- Verifica se tem 11 digitos e se só tem números (usei o REGEXP_LIKE E O LENGTH)
    IF LENGTH(CPF) != 11 OR NOT REGEXP_LIKE(CPF, '^\d{11}$') THEN
        RETURN FALSE;
    END IF;

    -- Verifica se todos os numeros sao iguais (ex 00000000000, 11111111111)
    IF CPF = LPAD(SUBSTR(CPF, 1, 1), 11, SUBSTR(CPF, 1, 1)) THEN
        RETURN FALSE;
    END IF;

    -- Calcular primeiro digito verificador (ex 111.111.111-X1 verifica o digito X)
    -- Para esse calculo deve-se multiplicar os 9 primeiros digitos do cpf (usei SUBSTR para
    -- separar os dígitos e depois transformar en número), um por um, por valores
    -- decrescentes de 10 até 2 (11-1, 11-2,..., 11-9). Depois verifica-se o resto da
    -- divisão da soma por 11. Se o resto for menor ou igual a 1 o penúltimod ígito
    -- deve ser igual a zero. E se o resto for maior que 2 então o penúltimo dígito
    -- deve ser igual a diferença entre 11 e o valor do resto (multipliquei por 10, para facilitar).
    FOR I IN 1..9
        LOOP
            SUM1 := SUM1 + TO_NUMBER(SUBSTR(CPF, I, 1)) * (11 - I);
        END LOOP;
    DIGIT1 := (SUM1 * 10) MOD 11;
    IF DIGIT1 = 10 THEN
        DIGIT1 := 0;
    END IF;

    -- Verifica se o primeiro digito digitado está correto
    IF DIGIT1 != TO_NUMBER(SUBSTR(CPF, 10, 1)) THEN
        RETURN FALSE;
    END IF;

    -- Calcular segundo digito verificador
    -- Para esse cálculo fazemos a soma como no outro, porem a multiplicação começa a partir de 11, e
    -- adiciona-se  na soma o primeiro dígito verificador (multiplicação dos 10 primeiros digitos do cpf).
    -- Depois se verifica o resto da divisão por 11, como no anterior.
    FOR I IN 1..10
        LOOP
            SUM2 := SUM2 + TO_NUMBER(SUBSTR(CPF, I, 1)) * (12 - I);
        END LOOP;
    DIGIT2 := (SUM2 * 10) MOD 11;
    IF DIGIT2 = 10 THEN
        DIGIT2 := 0;
    END IF;

    -- Verifica se o segundo digito digitado está correto
    IF DIGIT2 != TO_NUMBER(SUBSTR(CPF, 11, 1)) THEN
        RETURN FALSE;
    END IF;

    -- Se tudo passar, é valido
    RETURN TRUE;
EXCEPTION
    WHEN OTHERS THEN
        RAISE_APPLICATION_ERROR(2000, 'ERRO DESCONHECIDO.');
        RETURN FALSE;
END FUN_VALIDA_CPF;
/
--------------------------------------------------------------------------------------------------------------------------------
-- Validação de CNPJ: FUN_VALIDA_CNPJ
DECLARE
    CNPJ_NUMBER VARCHAR2(14) := '28797727000160';
    IS_VALID    BOOLEAN;
BEGIN
    IS_VALID := FUN_VALIDA_CNPJ(CNPJ_NUMBER);
    IF IS_VALID THEN
        DBMS_OUTPUT.PUT_LINE('CNPJ is valid.');
    ELSE
        DBMS_OUTPUT.PUT_LINE('CNPJ is invalid.');
    END IF;
END;
/
--
CREATE OR REPLACE FUNCTION FUN_VALIDA_CNPJ(
    CNPJ VARCHAR
) RETURN BOOLEAN IS
    SUM1              NUMBER             := 0;
    SUM2              NUMBER             := 0;
    DIGIT1            NUMBER;
    DIGIT2            NUMBER;

    -- Essas são listas que contém os pesos para verificação dos CNPJ's de 2 a 9 de tras para frente
    WEIGHTS1 CONSTANT SYS.ODCINUMBERLIST := SYS.ODCINUMBERLIST(5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2);
    WEIGHTS2 CONSTANT SYS.ODCINUMBERLIST := SYS.ODCINUMBERLIST(6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2);
    I                 NUMBER;
BEGIN
    -- Verifica se tem 14 digitos e se só tem números
    IF LENGTH(CNPJ) != 14 OR NOT REGEXP_LIKE(CNPJ, '^\d{14}$') THEN
        RETURN FALSE;
    END IF;

    -- Verifica se todos os numeros sao iguais (ex 00000000000, 11111111111)
    IF CNPJ = LPAD(SUBSTR(CNPJ, 1, 1), 14, SUBSTR(CNPJ, 1, 1)) THEN
        RETURN FALSE;
    END IF;

    -- Calcular primeiro digito verificador (o penúltimo número) se multiplica os 12 primeiros
    -- números pelos pesos das lista 1, um por um e somam-se os resultados. Depois verifica-se o resto da
    -- divisão dessa soma por 11. Se o resto for menor ou igual a 1 o penúltimod dígito
    -- deve ser igual a zero. E se o resto for maior que 2 então o penúltimo dígito deve ser igual a
    -- diferença entre 11 e o valor do resto (Essa conta é feita direto aqui: DIGIT1 := (SUM1 * 10) MOD 11).
    FOR I IN 1..12
        LOOP
            SUM1 := SUM1 + TO_NUMBER(SUBSTR(CNPJ, I, 1)) * WEIGHTS1(I);
        END LOOP;
    DIGIT1 := (SUM1 * 10) MOD 11;
    IF DIGIT1 = 10 THEN
        DIGIT1 := 0;
    END IF;

    -- Verifica se o primeiro digito digitado está correto
    IF DIGIT1 != TO_NUMBER(SUBSTR(CNPJ, 13, 1)) THEN
        RETURN FALSE;
    END IF;

    -- Calcular segundo digito verificador. Se multiplica os 13 primeiros
    -- números pelos pesos das lista 1, um por um e somam-se os resultados.
    -- Depois se verifica o resto da divisão por 11, como no anterior.
    FOR I IN 1..13
        LOOP
            SUM2 := SUM2 + TO_NUMBER(SUBSTR(CNPJ, I, 1)) * WEIGHTS2(I);
        END LOOP;
    DIGIT2 := (SUM2 * 10) MOD 11;
    IF DIGIT2 = 10 THEN
        DIGIT2 := 0;
    END IF;

    -- Verifica se o segundo digito digitado está correto
    IF DIGIT2 != TO_NUMBER(SUBSTR(CNPJ, 14, 1)) THEN
        RETURN FALSE;
    END IF;

    -- Se tudo passar, é valido
    RETURN TRUE;
END FUN_VALIDA_CNPJ;
/
--------------------------------------------------------------------------------------------------------------------------------
-- Validação de Nascimento: FUN_VALIDA_NASCIMENTO
DECLARE
    data_nasc DATE := TO_DATE('01/01/1900');
    is_valid  BOOLEAN;
BEGIN
    is_valid := FUN_VALIDA_NASCIMENTO(data_nasc);
    IF is_valid THEN
        DBMS_OUTPUT.PUT_LINE('Date is valid.');
    ELSE
        DBMS_OUTPUT.PUT_LINE('Date is invalid.');
    END IF;
END;
/
--
CREATE OR REPLACE FUNCTION FUN_VALIDA_NASCIMENTO(DATA_NASCIMENTO DATE) RETURN BOOLEAN IS
BEGIN
    IF DATA_NASCIMENTO < TO_DATE('01/01/1900') OR DATA_NASCIMENTO > CURRENT_DATE THEN
        RETURN FALSE;
    END IF;
    RETURN TRUE;
END;
/
--------------------------------------------------------------------------------------------------------------------------------
-- Validação de Telefone: FUN_VALIDA_TELEFONE
DECLARE
    telefone VARCHAR2(11) := '11987664790';
    is_valid BOOLEAN;
BEGIN
    is_valid := FUN_VALIDA_TELEFONE(telefone);
    IF is_valid THEN
        DBMS_OUTPUT.PUT_LINE('telefone is valid.');
    ELSE
        DBMS_OUTPUT.PUT_LINE('telefone is invalid.');
    END IF;
END;
/
--
CREATE OR REPLACE FUNCTION FUN_VALIDA_TELEFONE(TELEFONE VARCHAR2) RETURN BOOLEAN IS
BEGIN
    -- Verifica se tem 11 digitos e se só tem números
    IF LENGTH(TELEFONE) != 11 OR NOT REGEXP_LIKE(TELEFONE, '^\d{11}$') THEN
        RETURN FALSE;
    END IF;
    RETURN TRUE;
END;
/
--------------------------------------------------------------------------------------------------------------------------------
-- Validação de Email: FUN_VALIDA_EMAIL
DECLARE
    email_test VARCHAR2(100);
    is_valid   BOOLEAN;
BEGIN
    -- Test cases
    FOR email_test IN (
        SELECT 'user@example.com' AS email
        FROM dual
        UNION ALL
        SELECT 'user.name@domain.co.uk'
        FROM dual
        UNION ALL
        SELECT 'user-name@domain.org'
        FROM dual
        UNION ALL
        SELECT 'user@.com'
        FROM dual
        UNION ALL
        SELECT '@domain.com'
        FROM dual
        UNION ALL
        SELECT 'user@domain'
        FROM dual
        UNION ALL
        SELECT 'user@domain..com'
        FROM dual
        )
        LOOP
            is_valid := FUN_VALIDA_EMAIL(email_test.email);
            DBMS_OUTPUT.PUT_LINE('Email: ' || email_test.email || ' | Valid: ' || CASE WHEN is_valid THEN 'TRUE' ELSE 'FALSE' END);
        END LOOP;
END;
/
--
CREATE OR REPLACE FUNCTION FUN_VALIDA_EMAIL(EMAIL VARCHAR2) RETURN BOOLEAN IS
BEGIN
    /*
     Regex:
        ^: Marca o início da string, garantindo que a verificação comece do início.
        [A-Za-z0-9._%-]+: Representa a parte local do e-mail (antes do @), permitindo letras maiúsculas e minúsculas (A-Za-z), números (0-9), ponto (.), sublinhado (_), percentual (%) e hífen (-). O símbolo + exige que haja pelo menos um desses caracteres.
        @: Exige o símbolo @ separando a parte local do domínio.
        [A-Za-z0-9-]+: Representa o primeiro nível do domínio (após o @), permitindo letras (A-Za-z), números (0-9) e hífen (-), mas não pontos. O + indica que precisa haver pelo menos um desses caracteres.
        (\.[A-Za-z0-9-]+)*: Permite subdomínios adicionais precedidos por um ponto (.), seguidos por letras, números ou hífens. O * indica que essa parte é opcional e pode ocorrer várias vezes, mas sempre com um ponto seguido de caracteres válidos.
        \.[A-Za-z]{2,}$: Exige um ponto (.) seguido de pelo menos duas letras no final da string, representando a extensão do domínio (como .com ou .br).
        $: Marca o final da string, garantindo que toda a string do e-mail se encaixe no padrão.
    */
    RETURN REGEXP_LIKE(EMAIL, '^[A-Za-z0-9._%-]+@[A-Za-z0-9-]+(\.[A-Za-z0-9-]+)*\.[A-Za-z]{2,}$', 'c');
EXCEPTION
    WHEN OTHERS THEN
        RETURN FALSE;
END;
/

--------------------------------------------------------------------------------------------------------------------------------
-- Validação de Telefone: FUN_VALIDA_CEP
DECLARE
    cep_test VARCHAR2(10);
    is_valid BOOLEAN;
BEGIN
    -- Casos de teste
    FOR cep_test IN (
        SELECT '12345678' AS cep FROM dual
        UNION ALL
        SELECT '1234' FROM dual
        UNION ALL
        SELECT '123456789' FROM dual
        UNION ALL
        SELECT 'ABCDE678' FROM dual
    )
    LOOP
        is_valid := FUN_VALIDA_CEP(cep_test.cep);
        DBMS_OUTPUT.PUT_LINE('CEP: ' || cep_test.cep || ' | Valid: ' || CASE WHEN is_valid THEN 'TRUE' ELSE 'FALSE' END);
    END LOOP;
END;
/
--

CREATE OR REPLACE FUNCTION FUN_VALIDA_CEP(
    CEP VARCHAR2
) RETURN BOOLEAN IS
BEGIN
    -- Verifica se o CEP possui exatamente 8 dígitos numéricos
    IF LENGTH(CEP) = 8 AND REGEXP_LIKE(CEP, '^\d{8}$') THEN
        RETURN TRUE;
    ELSE
        RETURN FALSE;
    END IF;
END FUN_VALIDA_CEP;
/

--------------------------------------------------------------------------------------------------------------------------------