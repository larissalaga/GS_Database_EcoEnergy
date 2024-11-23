BEGIN
    inserir_dados_gs24();
END;
--
CREATE OR REPLACE PROCEDURE inserir_dados_gs24 AUTHID CURRENT_USER IS
BEGIN
    -- Inser��o de dados na tabela t_gs24_empresa
    insert_empresa('41357874000188', 'SolarTech Energia', '11999990001', 'Instala��o de sistemas fotovoltaicos');
    insert_empresa('47750105000121', 'Energiza Solu��es', '11999990011', 'Consultoria em energia renov�vel');
    insert_empresa('96776703000185', 'Fotovoltaica Brasil', '11999990021', 'Venda de equipamentos solares');
    insert_empresa('77570972000146', 'Energia Pura', '11999990031', 'Instala��o e manuten��o');
    insert_empresa('86230430000115', 'SolarMax', '11999990041', 'Consultoria e instala��o solar');
    insert_empresa('78842871000140', 'Energia Nova', '11999990051', 'Desenvolvimento de tecnologia solar');
    insert_empresa('11591591000135', 'Solarium Engenharia', '11999990061', 'Projetos de energia solar');
    insert_empresa('49006454000169', 'Luz do Sol', '11999990071', 'Consultoria em efici�ncia energ�tica');
    insert_empresa('97676111000154', 'HelioPower', '11999990081', 'Instala��o de pain�is solares');
    insert_empresa('61408356000153', 'EcoSol', '11999990091', 'Manuten��o de sistemas fotovoltaicos');


    -- Inser��o de dados na tabela t_gs24_usuario (2 usu�rios por empresa)
    insert_usuario('Jo�o Silva', 'joao.silva@solartech.com', '11988880001', '37748831068', 1);
    insert_usuario('Ana Costa', 'ana.costa@solartech.com', '11988880002', '24948922013', 1);

    insert_usuario('Carlos Pereira', 'carlos.pereira@energiza.com', '11988880011', '74826544022', 2);
    insert_usuario('Mariana Souza', 'mariana.souza@energiza.com', '11988880012', '13373851076', 2);

    insert_usuario('Rafael Oliveira', 'rafael.oliveira@fotovoltaica.com', '11988880021', '10103571078', 3);
    insert_usuario('Beatriz Lima', 'beatriz.lima@fotovoltaica.com', '11988880022', '51775884082', 3);

    insert_usuario('Gustavo Santos', 'gustavo.santos@energiapura.com', '11988880031', '15436064078', 4);
    insert_usuario('Fernanda Ribeiro', 'fernanda.ribeiro@energiapura.com', '11988880032', '48443262010', 4);

    insert_usuario('Roberto Almeida', 'roberto.almeida@solarmax.com', '11988880041', '95548937014', 5);
    insert_usuario('Luciana Martins', 'luciana.martins@solarmax.com', '11988880042', '22491236087', 5);


    -- Inser��o de dados na tabela t_gs24_usuario (5 usu�rios dom�sticos)
    insert_usuario('Paulo Henrique', 'paulo.henrique@example.com', '11997770001', '90610575074', NULL);
    insert_usuario('Juliana Andrade', 'juliana.andrade@example.com', '11997770002', '34439470009', NULL);
    insert_usuario('Renato Alves', 'renato.alves@example.com', '11997770003', '96880609090', NULL);
    insert_usuario('Fernanda Silva', 'fernanda.silva@example.com', '11997770004', '91395593000', NULL);
    insert_usuario('Thiago Moreira', 'thiago.moreira@example.com', '11997770005', '61697319076', NULL);


    -- Inser��o de dois endere�os para cada empresa
    insert_endereco('01001000', 'Pra�a da S�', 100, 'Centro', 'S�o Paulo', 'SP', 'Brasil', NULL, 1);
    insert_endereco('05010000', 'Rua Heitor Penteado', 200, 'Vila Madalena', 'S�o Paulo', 'SP', 'Brasil', NULL, 1);

    insert_endereco('30130001', 'Rua Fernandes Tourinho', 150, 'Savassi', 'Belo Horizonte', 'MG', 'Brasil', NULL, 2);
    insert_endereco('30140071', 'Avenida Afonso Pena', 300, 'Funcion�rios', 'Belo Horizonte', 'MG', 'Brasil', NULL, 2);

    insert_endereco('40020000', 'Rua Chile', 400, 'Centro', 'Salvador', 'BA', 'Brasil', NULL, 3);
    insert_endereco('40140110', 'Avenida Sete de Setembro', 450, 'Barra', 'Salvador', 'BA', 'Brasil', NULL, 3);

    insert_endereco('60125000', 'Avenida Santos Dumont', 600, 'Aldeota', 'Fortaleza', 'CE', 'Brasil', NULL, 4);
    insert_endereco('60165080', 'Avenida Beira Mar', 700, 'Meireles', 'Fortaleza', 'CE', 'Brasil', NULL, 4);

    insert_endereco('70297010', 'Rua das Palmeiras', 800, 'Asa Sul', 'Bras�lia', 'DF', 'Brasil', NULL, 5);
    insert_endereco('70804510', 'Avenida W3 Norte', 850, 'Asa Norte', 'Bras�lia', 'DF', 'Brasil', NULL, 5);


    -- Inser��o de dois endere�os para cada usu�rio dom�stico
    insert_endereco('04534011', 'Rua Funchal', 100, 'Vila Ol�mpia', 'S�o Paulo', 'SP', 'Brasil', 11, NULL);
    insert_endereco('04089000', 'Avenida Santo Amaro', 200, 'Brooklin', 'S�o Paulo', 'SP', 'Brasil', 11, NULL);

    insert_endereco('30110060', 'Rua da Bahia', 300, 'Centro', 'Belo Horizonte', 'MG', 'Brasil', 12, NULL);
    insert_endereco('30130180', 'Rua dos Timbiras', 400, 'Centro', 'Belo Horizonte', 'MG', 'Brasil', 12, NULL);

    insert_endereco('40010000', 'Avenida Sete de Setembro', 500, 'Centro', 'Salvador', 'BA', 'Brasil', 13, NULL);
    insert_endereco('40301350', 'Rua Direta da Piedade', 600, 'Piedade', 'Salvador', 'BA', 'Brasil', 13, NULL);

    insert_endereco('60130160', 'Rua Padre Valdevino', 700, 'Aldeota', 'Fortaleza', 'CE', 'Brasil', 14, NULL);
    insert_endereco('60140110', 'Avenida Dom Lu�s', 800, 'Meireles', 'Fortaleza', 'CE', 'Brasil', 14, NULL);

    insert_endereco('71615060', 'Avenida das Arauc�rias', 900, '�guas Claras', 'Bras�lia', 'DF', 'Brasil', 15, NULL);
    insert_endereco('70350700', 'SQS 308 Bloco G', 1000, 'Asa Sul', 'Bras�lia', 'DF', 'Brasil', 15, NULL);

       -- Inser��o de uma simula��o para cada endere�o de usu�rio dom�stico
    insert_simulacao(20000, 700, SYSDATE, 400, 60, 6, 700, 5, 1, '90610575074', 11);
    insert_simulacao(25000, 920, SYSDATE, 520, 72, 7.2, 820, 3, 1, '90610575074', 12);
    insert_simulacao(26000, 950, SYSDATE, 540, 74, 7.4, 840, 3, 1, '34439470009', 13);
    insert_simulacao(27000, 980, SYSDATE, 560, 76, 7.6, 860, 2, 0, '34439470009', 14);
    insert_simulacao(28000, 1000, SYSDATE, 580, 78, 7.8, 880, 2, 1, '96880609090', 15);
    insert_simulacao(29000, 1050, SYSDATE, 600, 80, 8, 900, 2, 0, '96880609090', 16);
    insert_simulacao(21000, 750, SYSDATE, 420, 62, 6.2, 720, 4, 0, '91395593000', 17);
    insert_simulacao(22000, 800, SYSDATE, 450, 65, 6.5, 750, 4, 1, '91395593000', 18);
    insert_simulacao(23000, 850, SYSDATE, 470, 67, 6.7, 770, 3, 1, '61697319076', 19);
    insert_simulacao(24000, 900, SYSDATE, 500, 70, 7, 800, 3, 1, '61697319076', 20);

    -- Inser��o de dois or�amentos para cada simula��o
    insert_orcamento(15000, '30 dias', SYSDATE, 'Instala��o completa', 1, '41357874000188');
    insert_orcamento(15500, '45 dias', SYSDATE, 'Instala��o e suporte', 1, '47750105000121');

    insert_orcamento(16000, '30 dias', SYSDATE, 'Instala��o b�sica', 2, '96776703000185');
    insert_orcamento(16500, '40 dias', SYSDATE, 'Instala��o e manuten��o', 2, '77570972000146');

    insert_orcamento(17000, '25 dias', SYSDATE, 'Sistema completo', 3, '86230430000115');
    insert_orcamento(17500, '50 dias', SYSDATE, 'Manuten��o e suporte', 3, '41357874000188');

    insert_orcamento(18000, '35 dias', SYSDATE, 'Instala��o e consultoria', 4, '47750105000121');
    insert_orcamento(18500, '45 dias', SYSDATE, 'Consultoria adicional', 4, '96776703000185');

    insert_orcamento(19000, '20 dias', SYSDATE, 'Pacote completo', 5, '77570972000146');
    insert_orcamento(19500, '30 dias', SYSDATE, 'Pacote b�sico', 5, '86230430000115');

    insert_orcamento(20000, '28 dias', SYSDATE, 'Instala��o residencial', 6, '41357874000188');
    insert_orcamento(20500, '32 dias', SYSDATE, 'Instala��o comercial', 6, '47750105000121');

    insert_orcamento(21000, '26 dias', SYSDATE, 'Sistema avan�ado', 7, '96776703000185');
    insert_orcamento(21500, '29 dias', SYSDATE, 'Sistema b�sico', 7, '77570972000146');

    insert_orcamento(22000, '33 dias', SYSDATE, 'Consultoria solar', 8, '86230430000115');
    insert_orcamento(22500, '40 dias', SYSDATE, 'Suporte e manuten��o', 8, '41357874000188');

    insert_orcamento(23000, '25 dias', SYSDATE, 'Servi�o completo', 9, '47750105000121');
    insert_orcamento(23500, '35 dias', SYSDATE, 'Servi�o parcial', 9, '96776703000185');

    insert_orcamento(24000, '45 dias', SYSDATE, 'Instala��o de painel', 10, '77570972000146');
    insert_orcamento(24500, '50 dias', SYSDATE, 'Instala��o e suporte', 10, '86230430000115');
    -- Confirma��o das altera��es
    COMMIT;

END inserir_dados_gs24;

