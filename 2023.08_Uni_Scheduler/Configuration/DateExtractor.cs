using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2023._08_Uni_Scheduler.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class DateExtractor
    {

        public static string GetSqlCondition(string input)
        {
            //var clientCondition = GetCustomerCondition(input);
            //var dateCondition = getDateConditions(input);
            //var manufacturerCondition = GetManufacturerCondition(input);
            //var productCondition = GetProductCondition(input);
            var reportType = GetReportTypeCondition(input);            
            //if (string.IsNullOrEmpty(clientCondition)
            //    || string.IsNullOrEmpty(dateCondition)
            //    || string.IsNullOrEmpty(manufacturerCondition)
            //    || string.IsNullOrEmpty(productCondition))
            //    return string.Empty;

            return $@"{reportType}";
        }
        private static string GetCustomerCondition(string input)
        {
            var pattern = @"clientes?[\s:]*(([\w\s\.-]+,?)+)";
            var match = Regex.Match(input.ToLower(), pattern);

            if (!match.Success)
                return string.Empty;

            var items = match.Groups[1].Value.Split(',').Select(i => i.Trim()).ToArray();

            var codigos = new List<string>();
            var descricoes = new List<string>();
            var cnpjs = new List<string>();

            foreach (var item in items)
            {
                if (Regex.IsMatch(item, @"^\d+$")) // Código
                {
                    codigos.Add(item);
                }
                else if (Regex.IsMatch(item, @"^\d{14}$")) // CNPJ
                {
                    cnpjs.Add($"'{item}'");
                }
                else // Descrição
                {
                    descricoes.Add($"Cliente.Razao_Social LIKE '%{item}%'");
                }
            }

            var conditions = new List<string>();

            if (codigos.Any())
                conditions.Add($"Cliente.Codigo IN ({string.Join(",", codigos)})");

            if (descricoes.Any())
                conditions.Add($"({string.Join(" OR ", descricoes)})");

            if (cnpjs.Any())
                conditions.Add($"Cliente.Cgc_Cpf IN ({string.Join(",", cnpjs)})");

            return string.Join(" OR ", conditions);
        }

        private static string GetManufacturerCondition(string input)
        {
            // Ajustando o pattern para considerar "Fabricante", "Fabricantes", "Fornecedor" e "Fornecedores"
            var pattern = @"(fabricantes?|fornecedores?|fornecedor?)[\s:]*(([\w\s\.-]+,?)+)";
            var match = Regex.Match(input.ToLower(), pattern);

            if (!match.Success)
                return string.Empty;

            // Pegando o grupo correto após a alteração no pattern
            var items = match.Groups[2].Value.Split(',').Select(i => i.Trim()).ToArray();

            var codigos = new List<string>();
            var descricoes = new List<string>();
            var cnpjs = new List<string>();

            foreach (var item in items)
            {
                if (Regex.IsMatch(item, @"^\d+$")) // Código
                {
                    codigos.Add(item);
                }
                else if (Regex.IsMatch(item, @"^\d{14}$")) // CNPJ
                {
                    cnpjs.Add($"'{item}'");
                }
                else // Descrição
                {
                    descricoes.Add($"'%{item}%'");
                }
            }

            var conditions = new List<string>();

            if (codigos.Any())
                conditions.Add($"Fabricante.Codigo IN ({string.Join(",", codigos)})");

            if (descricoes.Any())
                conditions.Add($"({string.Join(" OR ", descricoes.Select(d => $"Fabricante.Fantasia LIKE {d}"))})");

            if (cnpjs.Any())
                conditions.Add($"Fabricante.Cgc_Cpf IN ({string.Join(",", cnpjs)})");

            return string.Join(" OR ", conditions);
        }
        private static string GetProductCondition(string input)
        {
            // Ajustando o pattern para considerar "Produtos" e "Produtop"
            var pattern = @"(produtos?)[\s:]*(([\w\s\.-]+,?)+)";
            var match = Regex.Match(input.ToLower(), pattern);

            if (!match.Success)
                return string.Empty;

            // Pegando o grupo correto após a alteração no pattern
            var items = match.Groups[2].Value.Split(',').Select(i => i.Trim()).ToArray();

            var codigos = new List<string>();
            var descricoes = new List<string>();
            var eans = new List<string>();

            foreach (var item in items)
            {
                if (Regex.IsMatch(item, @"^\d+$")) // Código
                {
                    codigos.Add(item);
                }
                else if (Regex.IsMatch(item, @"^\d{14}$") 
                     ||  Regex.IsMatch(item, @"^\d{13}$")
                     ||  Regex.IsMatch(item, @"^\d{12}$")
                     ||  Regex.IsMatch(item, @"^\d{8}$"))
                      // ean
                {
                    eans.Add($"'{item}'");
                }
                else // Descrição
                {
                    descricoes.Add($"'%{item}%'");
                }
            }

            var conditions = new List<string>();

            if (codigos.Any())
                conditions.Add($"Produto.Codigo IN ({string.Join(",", codigos)})");

            if (descricoes.Any())
                conditions.Add($"({string.Join(" OR ", descricoes.Select(d => $"Produto.Descricao LIKE {d}"))})");

            if (eans.Any())
                conditions.Add($"Produto.Cod_EAN IN ({string.Join(",", eans)})");

            return string.Join(" OR ", conditions);
        }
        private static string GetStateCondition(string input)
        {
            // Ajustando o pattern para considerar "Estado", "Estados", "UF" e "UFS"
            var pattern = @"(estados?|ufs?)[\s:]*(([\w\s\.-]+,?)+)";
            var match = Regex.Match(input.ToLower(), pattern);

            if (!match.Success)
                return string.Empty;

            // Pegando o grupo correto após a alteração no pattern
            var items = match.Groups[2].Value.Split(',').Select(i => i.Trim()).ToArray();

            var ufs = new List<string>();
            var descricoes = new List<string>();

            foreach (var item in items)
            {
                if (item.Length == 2) // UF
                {
                    ufs.Add($"'{item.ToUpper()}'");
                }
                else if (item.Length > 2) // Descrição
                {
                    descricoes.Add($"'%{item}%'");
                }
            }

            var conditions = new List<string>();

            if (ufs.Any())
                conditions.Add($"Estado.Codigo IN ({string.Join(",", ufs)})");

            if (descricoes.Any())
                conditions.Add($"({string.Join(" OR ", descricoes.Select(d => $"Estado.Descricao LIKE {d}"))})");

            return string.Join(" OR ", conditions);
        }

        private static string GetClientTypeCondition(string input)
        {
            // Convertendo toda a entrada para minúsculas para facilitar o casamento de padrões
            input = input.ToLower();

            // Inicializa as listas para guardar os tipos de cliente
            var tiposPrivados = new List<string>();
            var tiposPublicos = new List<string>();

            // Verifica se os tipos de cliente privados ou públicos são mencionados na entrada
            if (Regex.IsMatch(input, @"(privado|priv)\b"))
            {
                tiposPrivados.Add("F");
                tiposPrivados.Add("N");
            }

            if (Regex.IsMatch(input, @"(público|pub)\b"))
            {
                tiposPublicos.Add("P");
                tiposPublicos.Add("M");
                tiposPublicos.Add("E");
            }

            // Monta a condição SQL
            var conditions = new List<string>();

            if (tiposPrivados.Any())
                conditions.Add($"Cliente.Tipo_Consumidor IN ('{string.Join("','", tiposPrivados.Distinct())}')");

            if (tiposPublicos.Any())
                conditions.Add($"Cliente.Tipo_Consumidor IN ('{string.Join("','", tiposPublicos.Distinct())}')");

            return string.Join(" OR ", conditions);
        }
        public static string GetReportTypeCondition(string input)
        {

            var clientCondition = GetCustomerCondition(input);
            var dateCondition = getDateConditions(input);
            var manufacturerCondition = GetManufacturerCondition(input);
            var productCondition = GetProductCondition(input);
            var stateCondition = GetStateCondition(input);
            var typeClientCondition = GetClientTypeCondition(input);

            // Mapeamento das palavras-chave para as condições e suas respectivas consultas SQL
            var reportTypeMap = new Dictionary<string, Tuple<string[], string>>
    {
        {"Custo do Estoque", new Tuple<string[], string>(new[] {"Custo do Estoque", "Estoque Custo", "Custo Estoque", "EST_Custo"}                        
        , $@"teste
")},        

        {"Ranking de Vendas por Estado", new Tuple<string[], string>(new[] {"Ranking de Vendas por Estado", "Ranking de Venda por Estado"}, $@"
WITH RankedData AS (
    SELECT
        Estado.Codigo [UF],
        SUM(NF_Saida_Itens.Qtd_Produto) AS [Qtd. Total],
        SUM(NF_Saida_Itens.Vlr_TotItem) AS [Vlr. Total]
    FROM [DMD].dbo.[NFSCB] NF_Saida
    INNER JOIN [DMD].dbo.[NFSIT] NF_Saida_Itens ON NF_Saida.num_nota = NF_Saida_Itens.num_nota
    INNER JOIN [DMD].dbo.[PRODU] Produto ON NF_Saida_Itens.cod_produto = Produto.Codigo
    INNER JOIN [DMD].dbo.[CLIEN] Cliente ON NF_Saida.cod_cliente = Cliente.Codigo
    INNER JOIN [DMD].dbo.[FABRI] Fabricante ON Fabricante.Codigo = Produto.Cod_Fabricante
    INNER JOIN [DMD].dbo.[ESTAD] Estado ON Estado.Codigo = NF_Saida.Estado
    WHERE NF_Saida.Status = 'F'
      AND NF_Saida.Cod_Cfo1 IN (5102, 5114, 5405, 5922, 6102, 6108, 6114, 6119, 6403, 6404, 6922)
      AND ({(string.IsNullOrEmpty(clientCondition) ? "1=1" : clientCondition)})
      AND ({(string.IsNullOrEmpty(productCondition) ? "1=1" : productCondition)})
      AND ({(string.IsNullOrEmpty(manufacturerCondition) ? "1=1" : manufacturerCondition)})
      AND ({(string.IsNullOrEmpty(dateCondition) ? "1=1" : dateCondition.Replace("date_Replace", "Dat_Emissao"))})      
      AND ({(string.IsNullOrEmpty(typeClientCondition) ? "1=1" : typeClientCondition)})     
    GROUP BY Estado.Codigo
)
SELECT
    RANK() OVER (ORDER BY [Qtd. Total] DESC, [Vlr. Total] DESC) AS Ranking,
    UF,
    [Qtd. Total],
    [Vlr. Total]
    
FROM RankedData
ORDER BY Ranking;
")},
        {"Ranking de Vendas por Produto", new Tuple<string[], string>(new[] {"Ranking de Vendas por Produto", "Ranking de Venda por Produtos"}, 
$@"WITH ProductRankedData AS (
    SELECT
        Produto.Codigo [Cód. Produto],
        Produto.Descricao [Produto],
        SUM(NF_Saida_Itens.Qtd_Produto) AS [Qtd. Total],
        SUM(NF_Saida_Itens.Vlr_TotItem) AS [Vlr. Total]
    FROM [DMD].dbo.[NFSCB] NF_Saida
    INNER JOIN [DMD].dbo.[NFSIT] NF_Saida_Itens ON NF_Saida.num_nota = NF_Saida_Itens.num_nota
    INNER JOIN [DMD].dbo.[PRODU] Produto ON NF_Saida_Itens.cod_produto = Produto.Codigo
    INNER JOIN [DMD].dbo.[CLIEN] Cliente ON NF_Saida.cod_cliente = Cliente.Codigo
    INNER JOIN [DMD].dbo.[FABRI] Fabricante ON Fabricante.Codigo = Produto.Cod_Fabricante
    INNER JOIN [DMD].dbo.[ESTAD] Estado ON Estado.Codigo = NF_Saida.Estado
    WHERE NF_Saida.Status = 'F'
      AND NF_Saida.Cod_Cfo1 IN (5102, 5114, 5405, 5922, 6102, 6108, 6114, 6119, 6403, 6404, 6922)
      AND ({(string.IsNullOrEmpty(clientCondition) ? "1=1" : clientCondition)})      
      AND ({(string.IsNullOrEmpty(manufacturerCondition) ? "1=1" : manufacturerCondition)})
      AND ({(string.IsNullOrEmpty(dateCondition) ? "1=1" : dateCondition.Replace("date_Replace", "Dat_Emissao"))})
      AND ({(string.IsNullOrEmpty(stateCondition) ? "1=1" : stateCondition)})
      AND ({(string.IsNullOrEmpty(typeClientCondition) ? "1=1" : typeClientCondition)})     
    GROUP BY Produto.Codigo, Produto.Descricao
)
SELECT
    RANK() OVER (ORDER BY [Qtd. Total] DESC, [Vlr. Total] DESC) AS Ranking,
	[Cód. Produto],
    [Produto],
    [Qtd. Total],
    [Vlr. Total]
    
FROM ProductRankedData
ORDER BY Ranking;")},

        {"Ranking de Vendas por Fabricante", new Tuple<string[], string>(new[] {"Ranking de Vendas por Fabricante", "Ranking de Venda por Fabricantes", "Ranking de Vendas por Fornecedor", "Ranking de Venda por Fornecedor"},
$@"
WITH ManufacturerRankedData AS (
    SELECT
        Fabricante.Codigo [Cód. Fabricante],
        Fabricante.Fantasia [Fabricante],
        SUM(NF_Saida_Itens.Qtd_Produto) AS [Qtd. Total],
        SUM(NF_Saida_Itens.Vlr_TotItem) AS [Vlr. Total]
    FROM [DMD].dbo.[NFSCB] NF_Saida
    INNER JOIN [DMD].dbo.[NFSIT] NF_Saida_Itens ON NF_Saida.num_nota = NF_Saida_Itens.num_nota
    INNER JOIN [DMD].dbo.[PRODU] Produto ON NF_Saida_Itens.cod_produto = Produto.Codigo
    INNER JOIN [DMD].dbo.[CLIEN] Cliente ON NF_Saida.cod_cliente = Cliente.Codigo
    INNER JOIN [DMD].dbo.[FABRI] Fabricante ON Fabricante.Codigo = Produto.Cod_Fabricante
    INNER JOIN [DMD].dbo.[ESTAD] Estado ON Estado.Codigo = NF_Saida.Estado
    WHERE NF_Saida.Status = 'F'
      AND NF_Saida.Cod_Cfo1 IN (5102, 5114, 5405, 5922, 6102, 6108, 6114, 6119, 6403, 6404, 6922)
      AND ({(string.IsNullOrEmpty(clientCondition) ? "1=1" : clientCondition)})
      AND ({(string.IsNullOrEmpty(productCondition) ? "1=1" : productCondition)})      
      AND ({(string.IsNullOrEmpty(dateCondition) ? "1=1" : dateCondition.Replace("date_Replace", "Dat_Emissao"))})
      AND ({(string.IsNullOrEmpty(stateCondition) ? "1=1" : stateCondition)})
      AND ({(string.IsNullOrEmpty(typeClientCondition) ? "1=1" : typeClientCondition)})     
    GROUP BY Fabricante.Codigo, Fabricante.Fantasia
)
SELECT
    RANK() OVER (ORDER BY [Vlr. Total] DESC, [Qtd. Total] DESC) AS ManufacturerRanking,
    [Cód. Fabricante],
    [Fabricante],
    [Qtd. Total],
    [Vlr. Total]
FROM ManufacturerRankedData
ORDER BY ManufacturerRanking;
"
)},
        {"Ranking de Vendas por Cliente", new Tuple<string[], string>(new[] {"Ranking de Vendas por Cliente", "Ranking de Venda por Clientes"},
        $@"
WITH ClientRankedData AS (
    SELECT
        Cliente.Codigo [Cód. Cliente],
        Cliente.Razao_Social [Cliente],
        SUM(NF_Saida_Itens.Qtd_Produto) AS [Qtd. Total],
        SUM(NF_Saida_Itens.Vlr_TotItem) AS [Vlr. Total]
    FROM [DMD].dbo.[NFSCB] NF_Saida
    INNER JOIN [DMD].dbo.[NFSIT] NF_Saida_Itens ON NF_Saida.num_nota = NF_Saida_Itens.num_nota
    INNER JOIN [DMD].dbo.[PRODU] Produto ON NF_Saida_Itens.cod_produto = Produto.Codigo
    INNER JOIN [DMD].dbo.[CLIEN] Cliente ON NF_Saida.cod_cliente = Cliente.Codigo
    INNER JOIN [DMD].dbo.[FABRI] Fabricante ON Fabricante.Codigo = Produto.Cod_Fabricante
    INNER JOIN [DMD].dbo.[ESTAD] Estado ON Estado.Codigo = NF_Saida.Estado
    WHERE NF_Saida.Status = 'F'
      AND NF_Saida.Cod_Cfo1 IN (5102, 5114, 5405, 5922, 6102, 6108, 6114, 6119, 6403, 6404, 6922)      
      AND ({(string.IsNullOrEmpty(productCondition) ? "1=1" : productCondition)})
      AND ({(string.IsNullOrEmpty(manufacturerCondition) ? "1=1" : manufacturerCondition)})
      AND ({(string.IsNullOrEmpty(dateCondition) ? "1=1" : dateCondition.Replace("date_Replace", "Dat_Emissao"))})
      AND ({(string.IsNullOrEmpty(stateCondition) ? "1=1" : stateCondition)})
      AND ({(string.IsNullOrEmpty(typeClientCondition) ? "1=1" : typeClientCondition)})     
    GROUP BY Cliente.Codigo, Cliente.Razao_Social
)
SELECT
    RANK() OVER (ORDER BY [Vlr. Total] DESC, [Qtd. Total] DESC) AS ClientRanking,
    [Cód. Cliente],
    [Cliente],
    [Qtd. Total],
    [Vlr. Total]
FROM ClientRankedData
ORDER BY ClientRanking;
"
)},
        {"Ranking de Vendas por Grupo de Clientes", new Tuple<string[], string>(new[] {"Ranking de Vendas por Grupo de Cliente", "Ranking de Vendas por Grupos de Cliente", "Ranking de Vendas por Grupos de Clientes"}, 
        $@"WITH ClientGroupRankedData AS (
    SELECT
        Grupo_Cliente.Des_GrpCli [Grupo de Clientes],
        SUM(NF_Saida_Itens.Qtd_Produto) AS [Qtd. Total],
        SUM(NF_Saida_Itens.Vlr_TotItem) AS [Vlr. Total]
    FROM [DMD].dbo.[NFSCB] NF_Saida
    INNER JOIN [DMD].dbo.[NFSIT] NF_Saida_Itens ON NF_Saida.num_nota = NF_Saida_Itens.num_nota
    INNER JOIN [DMD].dbo.[PRODU] Produto ON NF_Saida_Itens.cod_produto = Produto.Codigo
    INNER JOIN [DMD].dbo.[CLIEN] Cliente ON NF_Saida.cod_cliente = Cliente.Codigo
    INNER JOIN [DMD].dbo.[FABRI] Fabricante ON Fabricante.Codigo = Produto.Cod_Fabricante
    INNER JOIN [DMD].dbo.[ESTAD] Estado ON Estado.Codigo = NF_Saida.Estado
	INNER JOIN [DMD].dbo.[GRCLI] Grupo_Cliente ON Grupo_Cliente.Cod_GrpCli = Cliente.Cod_GrpCli
    WHERE NF_Saida.Status = 'F'
      AND NF_Saida.Cod_Cfo1 IN (5102, 5114, 5405, 5922, 6102, 6108, 6114, 6119, 6403, 6404, 6922)      
      AND ({(string.IsNullOrEmpty(productCondition) ? "1=1" : productCondition)})
      AND ({(string.IsNullOrEmpty(manufacturerCondition) ? "1=1" : manufacturerCondition)})
      AND ({(string.IsNullOrEmpty(dateCondition) ? "1=1" : dateCondition.Replace("date_Replace", "Dat_Emissao"))})
      AND ({(string.IsNullOrEmpty(stateCondition) ? "1=1" : stateCondition)})
      AND ({(string.IsNullOrEmpty(typeClientCondition) ? "1=1" : typeClientCondition)})     
    GROUP BY Grupo_Cliente.Des_GrpCli
)
SELECT
    RANK() OVER (ORDER BY [Vlr. Total] DESC, [Qtd. Total] DESC) AS ClientGroupRanking,
    [Grupo de Clientes],
    [Qtd. Total],
    [Vlr. Total]
FROM ClientGroupRankedData
ORDER BY ClientGroupRanking;")},

        {"Ranking de Vendas por Tipo de Consumidor", new Tuple<string[], string>(new[] {"Ranking de Vendas por Tipos de Consumidor", "Ranking de Vendas por Tipo de Consumidor", "Ranking de Vendas por Tipos de Consumidor", "Ranking de Vendas por Esfera", "Ranking de Vendas por Esferas"}, 
        $@"
WITH ConsumerTypeRankedData AS (
    SELECT
        CASE (Cliente.Tipo_Consumidor)
		WHEN 'F' THEN 'Cliente Público Final' 
		WHEN 'N' THEN 'Cliente Público Não Final' 
		WHEN 'P' THEN 'Órgão Público Federal' 
		WHEN 'M' THEN 'Órgão Público Municipal' 
		WHEN 'E' THEN 'Órgão Público Estadual' 
		END [Tipo de Consumidor],
        SUM(NF_Saida_Itens.Qtd_Produto) AS [Qtd. Total],
        SUM(NF_Saida_Itens.Vlr_TotItem) AS [Vlr. Total]
    FROM [DMD].dbo.[NFSCB] NF_Saida
    INNER JOIN [DMD].dbo.[NFSIT] NF_Saida_Itens ON NF_Saida.num_nota = NF_Saida_Itens.num_nota
    INNER JOIN [DMD].dbo.[PRODU] Produto ON NF_Saida_Itens.cod_produto = Produto.Codigo
    INNER JOIN [DMD].dbo.[CLIEN] Cliente ON NF_Saida.cod_cliente = Cliente.Codigo
    INNER JOIN [DMD].dbo.[FABRI] Fabricante ON Fabricante.Codigo = Produto.Cod_Fabricante
    INNER JOIN [DMD].dbo.[ESTAD] Estado ON Estado.Codigo = NF_Saida.Estado
    WHERE NF_Saida.Status = 'F'
      AND NF_Saida.Cod_Cfo1 IN (5102, 5114, 5405, 5922, 6102, 6108, 6114, 6119, 6403, 6404, 6922)      
      AND ({(string.IsNullOrEmpty(productCondition) ? "1=1" : productCondition)})
      AND ({(string.IsNullOrEmpty(manufacturerCondition) ? "1=1" : manufacturerCondition)})
      AND ({(string.IsNullOrEmpty(dateCondition) ? "1=1" : dateCondition.Replace("date_Replace", "Dat_Emissao"))})
      AND ({(string.IsNullOrEmpty(stateCondition) ? "1=1" : stateCondition)})
      AND ({(string.IsNullOrEmpty(typeClientCondition) ? "1=1" : typeClientCondition)})     
    GROUP BY Cliente.Tipo_Consumidor
)
SELECT
    RANK() OVER (ORDER BY [Vlr. Total] DESC, [Qtd. Total] DESC) AS ConsumerTypeRanking,
    [Tipo de Consumidor],
    [Qtd. Total],
    [Vlr. Total]
FROM ConsumerTypeRankedData
ORDER BY ConsumerTypeRanking;

")},
        {"Ranking de Vendas por Vendedor", new Tuple<string[], string>(new[] {"Ranking de Vendas por Vendedor", "Ranking de Vendas por Vendedores"}, 
        $@"WITH SellerRankedData AS (
    SELECT
        Vendedor.Nome_Guerra [Vendedor],
        SUM(NF_Saida_Itens.Qtd_Produto) AS [Qtd. Total],
        SUM(NF_Saida_Itens.Vlr_TotItem) AS [Vlr. Total]
    FROM [DMD].dbo.[NFSCB] NF_Saida
    INNER JOIN [DMD].dbo.[NFSIT] NF_Saida_Itens ON NF_Saida.num_nota = NF_Saida_Itens.num_nota
    INNER JOIN [DMD].dbo.[PRODU] Produto ON NF_Saida_Itens.cod_produto = Produto.Codigo
    INNER JOIN [DMD].dbo.[CLIEN] Cliente ON NF_Saida.cod_cliente = Cliente.Codigo
    INNER JOIN [DMD].dbo.[FABRI] Fabricante ON Fabricante.Codigo = Produto.Cod_Fabricante
    INNER JOIN [DMD].dbo.[ESTAD] Estado ON Estado.Codigo = NF_Saida.Estado
	INNER JOIN [DMD].dbo.[VENDE] Vendedor ON Vendedor.Codigo = NF_Saida.Cod_Vendedor
    WHERE NF_Saida.Status = 'F'
      AND NF_Saida.Cod_Cfo1 IN (5102, 5114, 5405, 5922, 6102, 6108, 6114, 6119, 6403, 6404, 6922)
      AND ({(string.IsNullOrEmpty(clientCondition) ? "1=1" : clientCondition)})
      AND ({(string.IsNullOrEmpty(productCondition) ? "1=1" : productCondition)})
      AND ({(string.IsNullOrEmpty(manufacturerCondition) ? "1=1" : manufacturerCondition)})
      AND ({(string.IsNullOrEmpty(dateCondition) ? "1=1" : dateCondition.Replace("date_Replace", "Dat_Emissao"))})
      AND ({(string.IsNullOrEmpty(stateCondition) ? "1=1" : stateCondition)})
      AND ({(string.IsNullOrEmpty(typeClientCondition) ? "1=1" : typeClientCondition)})     
    GROUP BY Vendedor.Nome_Guerra
)
SELECT
    RANK() OVER (ORDER BY [Vlr. Total] DESC, [Qtd. Total] DESC) AS SellerRanking,
    [Vendedor],
    [Qtd. Total],
    [Vlr. Total]
FROM SellerRankedData
ORDER BY SellerRanking;")},

        {"Estoque", new Tuple<string[], string>(new[] {"Estoque"}, 
        $@"SELECT Produto.Codigo									[Cód. Produto],
                  Produto.Descricao								    [Produto],
                  Produto.Qtd_Disponivel + Produto.Qtd_Solicitado	[Qtd. Disponível],
                  Produto.cod_locfis								[Local],
           	      Fabricante.Fantasia								[Fornecedor]
           FROM   [DMD].dbo.[PRODU] Produto
           JOIN   [DMD].dbo.[FABRI] Fabricante ON Fabricante.Codigo = Produto.Cod_Fabricante
           WHERE 1=1
AND ({(string.IsNullOrEmpty(manufacturerCondition)? "1=1":manufacturerCondition)})
AND ({(string.IsNullOrEmpty(productCondition)? "1=1":productCondition)})
ORDER BY Produto.Codigo
")},
        {"Vendas", new Tuple<string[], string>(new[] {"Vendas", "Venda"}, 
        $@"SELECT
	   CONVERT(DATE,NF_Saida.Dat_Emissao) [Dat. Emissão] ,       
       Estado.Codigo [UF] ,
       NF_Saida.Cidade [Cidade] ,
       Produto.Codigo [Cód. Produto],
       Produto.Descricao [Produto],
       Fabricante.Fantasia [Fabricante] ,
       Cliente.Codigo [Cód. Cliente] ,
       Cliente.Razao_Social [Cliente] ,
       Cliente.Cgc_Cpf [CNPJ / CPF CLI] ,
       NF_Saida_Itens.Qtd_Produto [Qtd.Vendas] ,
       NF_Saida_Itens.Vlr_TotItem [Valor]
FROM	   [DMD].dbo.[NFSCB] NF_Saida
INNER JOIN [DMD].dbo.[NFSIT] NF_Saida_Itens ON NF_Saida.num_nota = NF_Saida_Itens.num_nota
INNER JOIN [DMD].dbo.[PRODU] Produto ON NF_Saida_Itens.cod_produto = Produto.Codigo
INNER JOIN [DMD].dbo.[CLIEN] Cliente ON NF_Saida.cod_cliente = Cliente.Codigo
INNER JOIN [DMD].dbo.[FABRI] Fabricante ON Fabricante.Codigo = Produto.Cod_Fabricante
INNER JOIN [DMD].dbo.[ESTAD] Estado ON Estado.Codigo = NF_Saida.Estado
WHERE NF_Saida.Status ='F'
  AND NF_Saida.Cod_Cfo1 IN (5102,
							5114,
							5405,
							5922,
							6102,
							6108,
							6114,
							6119,
							6403,
							6404,
							6922)
AND ({(string.IsNullOrEmpty(clientCondition)? "1=1":clientCondition)})
AND ({(string.IsNullOrEmpty(productCondition)? "1=1":productCondition)})
AND ({(string.IsNullOrEmpty(manufacturerCondition)? "1=1":manufacturerCondition)})
AND ({(string.IsNullOrEmpty(dateCondition)? "1=1":dateCondition.Replace("date_Replace","Dat_Emissao"))})
AND ({(string.IsNullOrEmpty(typeClientCondition) ? "1=1" : typeClientCondition)})     
AND ({(string.IsNullOrEmpty(stateCondition)? "1=1":stateCondition)})")}


    };

            // Verificar qual condição corresponde à entrada
            foreach (var pair in reportTypeMap)
            {
                if (pair.Value.Item1.Any(keyword => input.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    return pair.Value.Item2; // Retorna a consulta SQL correspondente
                }
            }

            return string.Empty; // Se não encontrar nenhuma correspondência
        }

        private static readonly Dictionary<string, int> MonthMap = new Dictionary<string, int>
    {
        {"janeiro", 1}, {"fevereiro", 2}, {"março", 3}, {"abril", 4},
        {"maio", 5}, {"junho", 6}, {"julho", 7}, {"agosto", 8},
        {"setembro", 9}, {"outubro", 10}, {"novembro", 11}, {"dezembro", 12}
    };
        public static string getDateConditions(string input)
        {
            input = input.ToLower();
            
            // Se as palavras "período de", "periodo de" ou "entre" estiverem presentes, comece a interpretar a data a partir dessa palavra
            var periodIndex = Regex.Match(input, @"per[ií]odo d[eo]|entre").Index;
            if (periodIndex > 0)
            {
                // Encontre o final da correspondência para começar a substring a partir daí
                var matchLength = Regex.Match(input, @"per[ií]odo d[eo]|entre").Length;
                input = input.Substring(periodIndex + matchLength);
            }

            // Primeira passagem: procurar qualquer padrão de data
            var datePattern = @"(\d{1,2}[-/]\d{1,2}[-/]\d{4}|\d{4}[-/]\d{1,2}[-/]\d{1,2}|\d{1,2} de [a-z]+ de \d{4}|[a-z]+ de \d{4}|[a-z]+ desse ano|[a-z]+ do ano passado|[a-z]+ do ano retrasado|\d{1,2} dias atrás|primeiro [a-z]+ de \d{4}|[a-z]+ até [a-z]+|dia \d{1,2} de [a-z]+|dia \d{1,2} de [a-z]+ de \d{4}|\d{4}|[1-2] semestre|[1-4] trimestre)";
            var matches = Regex.Matches(input, datePattern);

            // Se encontrarmos apenas o ano, faça uma segunda passagem para procurar uma data mais específica
            if (matches.Count == 2 && matches[0].Value.Length == 4 && matches[1].Value.Length == 4)
            {                
                datePattern = @"(\d{1,2} de [a-z]+ de \d{4})";
                var newMatches = Regex.Matches(input, datePattern);
                if (newMatches.Count == 2)
                {
                    matches = newMatches;
                }
            }






            if (input.Contains("até hoje") || input.Contains("a partir de"))
            {
                var startDate = ConvertToDateTime(matches[0].Value, true);
                var endDate = DateTime.Now;

                return $"date_Replace BETWEEN '{startDate:yyyyMMdd}' AND '{endDate:yyyyMMdd}'";
            }
            else if (matches.Count == 1)
            {
                var match = matches[0].Value;
                if (match.Contains("semestre") || match.Contains("trimestre"))
                {
                    var year = DateTime.Now.Year;
                    if (Regex.IsMatch(match, @"\d{4}"))
                    {
                        
                        year = int.Parse(Regex.Match(match, @"\d{4}").Value);
                    }

                    DateTime startDate, endDate;

                    if (match.Contains("primeiro semestre") || match.Contains("1 semestre"))
                    {
                        startDate = new DateTime(year, 1, 1);
                        endDate = new DateTime(year, 6, 30);
                    }
                    else if (match.Contains("segundo semestre") || match.Contains("2 semestre"))
                    {
                        startDate = new DateTime(year, 7, 1);
                        endDate = new DateTime(year, 12, 31);
                    }
                    else if (match.Contains("primeiro trimestre") || match.Contains("1 trimestre"))
                    {
                        startDate = new DateTime(year, 1, 1);
                        endDate = new DateTime(year, 3, 31);
                    }
                    else if (match.Contains("segundo trimestre") || match.Contains("2 trimestre"))// Segundo trimestre
                    {
                        startDate = new DateTime(year, 4, 1);
                        endDate = new DateTime(year, 6, 30);
                    }
                    else if (match.Contains("terceiro trimestre") || match.Contains("3 trimestre"))// Terceiro trimestre
                    {
                        startDate = new DateTime(year, 7, 1);
                        endDate = new DateTime(year, 9, 30);
                    }
                    else
                    {
                        

                        startDate = new DateTime(year, 10, 1);
                        
                        endDate = new DateTime(year, 12, 31);
                    }

                    return $"date_Replace BETWEEN '{startDate:yyyyMMdd}' AND '{endDate:yyyyMMdd}'";
                }
                else
                {
                    var startDate = ConvertToDateTime(matches[0].Value,false);
                    return $"date_Replace = '{startDate:yyyyMMdd}'";
                }
            }
            else if (matches.Count == 2)
            {

                var startDate = ConvertToDateTime(matches[0].Value, true);
                Console.WriteLine("Segunda data: " + matches[1].Value);
                var endDate = ConvertToDateTime(matches[1].Value, false);

                if (startDate > endDate)
                {
                    var temp = startDate;
                    startDate = endDate;
                    endDate = temp;
                }

                return $"date_Replace BETWEEN '{startDate:yyyyMMdd}' AND '{endDate:yyyyMMdd}'";
            }


            return "YEAR(date_Replace) = YEAR(GETDATE())";
        }


        private static DateTime ConvertToDateTime(string dateStr, bool startOfPeriod)
        {
            if (Regex.IsMatch(dateStr, @"^\d{1,2} de [a-z]+ de \d{4}$"))
            {
                var day = int.Parse(dateStr.Split(' ')[0]);
                var month = MonthMap[dateStr.Split(' ')[2]];
                var year = int.Parse(dateStr.Split(' ')[4]);

                Console.WriteLine($"Day: {day}, Month: {month}, Year: {year}");  // Adicione esta linha

                return new DateTime(year, month, day);
            }
            // Apenas mês e ano, como "janeiro de 2000"
            else if (MonthMap.ContainsKey(dateStr.Split(' ')[0]))
            {
                var month = MonthMap[dateStr.Split(' ')[0]];
                var year = int.Parse(dateStr.Split(' ')[dateStr.Split(' ').Length - 1]);
                if (startOfPeriod)
                {
                    return new DateTime(year, month, 1);
                }
                else
                {
                    var lastDay = DateTime.DaysInMonth(year, month);
                    return new DateTime(year, month, lastDay);
                }
            }
            // Apenas ano, como "2000"
            else if (Regex.IsMatch(dateStr, @"^\d{4}$"))
            {
                var year = int.Parse(dateStr);
                if (startOfPeriod)
                {
                    return new DateTime(year, 1, 1);
                }
                else
                {
                    return new DateTime(year, 12, 31);
                }
            }
            // Semestre
            else if (dateStr.Contains("primeiro semestre") || dateStr.Contains("1 semestre"))
            {
                var year = DateTime.Now.Year;
                if (Regex.IsMatch(dateStr, @"\d{4}"))
                {
                    year = int.Parse(Regex.Match(dateStr, @"\d{4}").Value);
                }
                return startOfPeriod ? new DateTime(year, 1, 1) : new DateTime(year, 6, 30);
            }
            else if (dateStr.Contains("segundo semestre") || dateStr.Contains("2 semestre"))
            {
                var year = DateTime.Now.Year;
                if (Regex.IsMatch(dateStr, @"\d{4}"))
                {
                    year = int.Parse(Regex.Match(dateStr, @"\d{4}").Value);
                }
                return startOfPeriod ? new DateTime(year, 7, 1) : new DateTime(year, 12, 31);
            }
            // Outros casos
            else if (DateTime.TryParse(dateStr, out DateTime date))
            {
                return date;
            }
            else if (dateStr.Contains("hoje"))
            {
                return DateTime.Now;
            }
            else if (dateStr.Contains("dias atrás"))
            {
                var days = int.Parse(Regex.Match(dateStr, @"\d+").Value);
                return DateTime.Now.AddDays(-days);
            }
            return DateTime.MinValue;
        }





    }
}
