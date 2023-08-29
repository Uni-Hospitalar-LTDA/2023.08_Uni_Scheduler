using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2023._08_Uni_Scheduler.Configuration
{
    using _2023._08_Uni_Scheduler.Domain.Entities;
    using DocumentFormat.OpenXml.Drawing;
    using DocumentFormat.OpenXml.Drawing.Diagrams;
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class DateExtractor
    {        
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
        private static string GetCustomerInterpretation(string input)
        {
            var pattern = @"clientes?[\s:]*(([\w\s\.-]+,?)+)";
            var match = Regex.Match(input.ToLower(), pattern);

            if (!match.Success)
                return "Todos os Clientes.";

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
                    cnpjs.Add(item);
                }
                else // Descrição
                {
                    descricoes.Add(item);
                }
            }

            var interpretations = new List<string>();

            if (codigos.Any())
                interpretations.Add($"Cliente -> Códigos: {string.Join(",", codigos)}");

            if (cnpjs.Any())
                interpretations.Add($"Cliente -> CNPJ: {string.Join(", ", cnpjs)}");

            if (descricoes.Any())
                interpretations.Add($"Cliente -> Descrição: {string.Join(", ", descricoes)}");

            return string.Join(" | ", interpretations);
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
        private static string GetManufacturerInterpretation(string input)
        {
            var pattern = @"(fabricantes?|fornecedores?|fornecedor?)[\s:]*(([\w\s\.-]+,?)+)";
            var match = Regex.Match(input.ToLower(), pattern);

            if (!match.Success)
                return "Todos os Fabricantes/Fornecedores.";

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
                    cnpjs.Add(item);
                }
                else // Descrição
                {
                    descricoes.Add(item);
                }
            }

            var interpretations = new List<string>();

            if (codigos.Any())
                interpretations.Add($"Fabricante -> Códigos: {string.Join(",", codigos)}");

            if (cnpjs.Any())
                interpretations.Add($"Fabricante -> CNPJ: {string.Join(", ", cnpjs)}");

            if (descricoes.Any())
                interpretations.Add($"Fabricante -> Descrição: {string.Join(", ", descricoes)}");

            return string.Join(" | ", interpretations);
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
        private static string GetProductInterpretation(string input)
        {
            var pattern = @"(produtos?)[\s:]*(([\w\s\.-]+,?)+)";
            var match = Regex.Match(input.ToLower(), pattern);

            if (!match.Success)
                return "Todos os Produtos.";

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
                     || Regex.IsMatch(item, @"^\d{13}$")
                     || Regex.IsMatch(item, @"^\d{12}$")
                     || Regex.IsMatch(item, @"^\d{8}$")) // EAN
                {
                    eans.Add(item);
                }
                else // Descrição
                {
                    descricoes.Add(item);
                }
            }

            var interpretations = new List<string>();

            if (codigos.Any())
                interpretations.Add($"Produto -> Códigos: {string.Join(",", codigos)}");

            if (eans.Any())
                interpretations.Add($"Produto -> EANs: {string.Join(", ", eans)}");

            if (descricoes.Any())
                interpretations.Add($"Produto -> Descrição: {string.Join(", ", descricoes)}");

            return string.Join(" | ", interpretations);
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
        private static string GetStateInterpretation(string input)
        {
            var pattern = @"(estados?|ufs?)[\s:]*(([\w\s\.-]+,?)+)";
            var match = Regex.Match(input.ToLower(), pattern);

            if (!match.Success)
                return "Todos os Estados";

            var items = match.Groups[2].Value.Split(',').Select(i => i.Trim()).ToArray();

            var ufs = new List<string>();
            var descricoes = new List<string>();

            foreach (var item in items)
            {
                if (item.Length == 2) // UF
                {
                    ufs.Add(item.ToUpper());
                }
                else if (item.Length > 2) // Descrição
                {
                    descricoes.Add(item);
                }
            }

            var interpretations = new List<string>();

            if (ufs.Any())
                interpretations.Add($"Estado -> UFs: {string.Join(", ", ufs)}");

            if (descricoes.Any())
                interpretations.Add($"Estado -> Descrição: {string.Join(", ", descricoes)}");

            return string.Join(" | ", interpretations);
        }

        private async static Task<SchedulerApp_Connection> GetConnectionCondition(string input)
        {
            // Convertendo toda a entrada para minúsculas para facilitar o casamento de padrões
            input = input.ToLower();            
           
            // Verifica se as conexões de Pernambuco são mencionadas na entrada
            if (Regex.IsMatch(input, @"(uni hospitalar ltda|uni hospitalar|uni pe|uni pernambuco|uni recife|pernambuco|pe)\b"))
            {
                return await SchedulerApp_Connection.getToClassByDescriptionAsync("PE");
            }

            // Verifica se as conexões de Ceará são mencionadas na entrada
            if (Regex.IsMatch(input, @"(uni hospitalar ceara|uni ceara|uni ce|ceará|ceara|ce)\b"))
            {
                return await SchedulerApp_Connection.getToClassByDescriptionAsync("CE");
            }

            // Verifica se as conexões de São Paulo são mencionadas na entrada
            if (Regex.IsMatch(input, @"(sp hospitalar|são paulo|sp hosp)\b"))
            {
                return await SchedulerApp_Connection.getToClassByDescriptionAsync("SP");
            }
                                       
            return await SchedulerApp_Connection.getToClassByDescriptionAsync("PE");
        }
        private static string GetConnectionInterpretation(string input)
        {
            // Convertendo toda a entrada para minúsculas para facilitar o casamento de padrões
            input = input.ToLower();

            var interpretations = new List<string>();

            // Verifica se as conexões de Pernambuco são mencionadas na entrada
            if (Regex.IsMatch(input, @"(uni hospitalar ltda|uni hospitalar|uni pe|uni pernambuco|uni recife|pernambuco|pe)\b"))
            {
                interpretations.Add("Conexão -> Pernambuco");
            }

            // Verifica se as conexões de Ceará são mencionadas na entrada
            if (Regex.IsMatch(input, @"(uni hospitalar ceara|uni ceara|uni ce|ceará|ceara|ce)\b"))
            {
                interpretations.Add("Conexão -> Ceará");
            }

            // Verifica se as conexões de São Paulo são mencionadas na entrada
            if (Regex.IsMatch(input, @"(sp hospitalar|são paulo|sp hosp)\b"))
            {
                interpretations.Add("Conexão -> São Paulo");
            }
            if (interpretations.Count == 0)
                interpretations.Add("PE");
            return string.Join(" | ", interpretations);
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
        private static string GetClientTypeInterpretation(string input)
        {
            // Convertendo toda a entrada para minúsculas para facilitar o casamento de padrões
            input = input.ToLower();

            var interpretations = new List<string>();

            // Verifica se os tipos de cliente privados são mencionados na entrada
            if (Regex.IsMatch(input, @"(privado|priv)\b"))
            {
                interpretations.Add("Cliente -> Tipo: Privado");
            }

            // Verifica se os tipos de cliente públicos são mencionados na entrada
            if (Regex.IsMatch(input, @"(público|pub)\b"))
            {
                interpretations.Add("Cliente -> Tipo: Público");
            }

            return string.Join(" | ", interpretations);
        }

        public static async  Task<Tuple<Archive,SchedulerApp_Connection>> GetReportTypeCondition(string input)
        {

            var clientCondition = GetCustomerCondition(input);
            var dateCondition = getDateConditions(input);
            var manufacturerCondition = GetManufacturerCondition(input);
            var productCondition = GetProductCondition(input);
            var stateCondition = GetStateCondition(input);
            var typeClientCondition = GetClientTypeCondition(input);
            var connection = await GetConnectionCondition(input);

            // Mapeamento das palavras-chave para as condições e suas respectivas consultas SQL
            var reportTypeMap = new Dictionary<string, Tuple<string[], string>>
    {
        {"Custo do Estoque "+DateTime.Now.ToString("dd-MM-yy"), new Tuple<string[], string>(new[]

        {"Custo do Estoque", "Custo d Estoque", "Custo do Etoque", "Cuto do Estoque", "Custo doEstoque", "Costo do Estoque", "Custo do Estoqu", "Custo de Estoque", "Custo do stock", "Custo Stock", "Custo do stok", "Custo Stocke", "Custo Stoq", "Custo d Stock", "Estoque Custo", "Estoqu Custo", "Etoque Custo", "Estoque Cuto", "Estoque Costo", "Estoque Cust", "Stock Custo", "Estok Custo", "Stocke Custo", "Stoq Custo", "Custo Estoque", "Custo Etoque", "Cuto Estoque", "Costo Estoque", "Custo Estoqu", "Custo Estok", "Custo Stocke", "Custo Stoq", "EST_Custo", "ESTCusto", "ET_Custo", "EST_Cust", "EST_Custp", "ES_Custo", "ESTCuto", "ESTCosto"}
        , $@"SELECT produto.codigo [Cód. Produto] ,
       descricao [Produto] ,
       qtd_disponivel+qtd_solicitado [Qtd. Disponível] ,
       Round (produto.prc_customedio,2) [Custo Médio],
       cod_locfis [Local] ,
       produto.prc_ultent [Prc. Última Compra],
       produto.dat_ultcompra [Dat. Última Compra] ,
       fabricante.fantasia [Fabricante]
FROM	dmd.dbo.produ produto 
join	dmd.dbo.fabri fabricante
ON produto.cod_fabricante = fabricante.codigo
WHERE  1=1
AND ({(string.IsNullOrEmpty(productCondition) ? "1=1" : productCondition)})
AND ({(string.IsNullOrEmpty(manufacturerCondition) ? "1=1" : manufacturerCondition)})
")},        

        {"Ranking de Vendas por Estado "+DateTime.Now.ToString("dd-MM-yy"), new Tuple<string[], string>(new[]


        {"Ranking de Venda por Estado", "Ranking de Venda por estados", "Ranking de vendas por Estado", "Rnking de Vendas por Estado", "Rankng de Vendas por Estado", "Ranking d Vendas por Estado", "Ranking de Vendas po Estado", "Ranking de Vendas por Estad", "Rank de Vendas por Estado", "Classificação de Vendas por Estado", "Lista de Vendas por Estado", "Ranking de Vendas p/ Estado", "Rank de Vendas p/ Estado", "Ranking de Vendas por Estado", "Rnking de Venda por Estado", "Rankng de Venda por Estado", "Ranking d Venda por Estado", "Ranking de Venda po Estado", "Ranking de Venda por Estad", "Rank de Venda por Estado", "Classificação de Venda por Estado", "Lista de Venda por Estado", "Ranking de Venda p/ Estado", "Rank de Venda p/ Estado"}

        , $@"
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
        {"Ranking de Vendas por Produto "+DateTime.Now.ToString("dd-MM-yy"), new Tuple<string[], string>(new[]

        {"Ranking de Vendas por Produto", "Ranking de Venda por Produtos", "Rnking de Vendas por Produto", "Rankng de Vendas por Produto", "Ranking d Vendas por Produto", "Ranking de Vendas po Produto", "Ranking de Vendas por Poduto", "Ranking de Vendas por Produt", "Rank de Vendas por Produto", "Classificação de Vendas por Produto", "Lista de Vendas por Produto", "Ranking de Vendas p/ Produto", "Rnking de Venda por Produtos", "Rankng de Venda por Produtos", "Ranking d Venda por Produtos", "Ranking de Venda po Produtos", "Ranking de Venda por Produts", "Rank de Venda por Produtos", "Classificação de Venda por Produtos", "Lista de Venda por Produtos", "Ranking de Venda p/ Produtos"}
        , 
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

        {"Ranking de Vendas por Fabricante", new Tuple<string[], string>(new[]
        {"Ranking de Vendas por Fabricante", "Rnking de Vendas por Fabricante", "Rankng de Vendas por Fabricante", "Ranking d Vendas por Fabricante", "Ranking de Vendas po Fabricante", "Ranking de Vendas por Fabrcante", "Ranking de Vendas por Fabricnte", "Rank de Vendas por Fabricante", "Classificação de Vendas por Fabricante", "Lista de Vendas por Fabricante", "Ranking de Vendas p/ Fabricante", "Ranking de Venda por Fabricantes", "Rnking de Venda por Fabricantes", "Rankng de Venda por Fabricantes", "Ranking d Venda por Fabricantes", "Ranking de Venda po Fabricantes", "Ranking de Venda por Fabrcantes", "Ranking de Venda por Fabricntes", "Rank de Venda por Fabricantes", "Classificação de Venda por Fabricantes", "Lista de Venda por Fabricantes", "Ranking de Venda p/ Fabricantes", "Ranking de Vendas por Fornecedor", "Rnking de Vendas por Fornecedor", "Rankng de Vendas por Fornecedor", "Ranking d Vendas por Fornecedor", "Ranking de Vendas po Fornecedor", "Ranking de Vendas por Fornecdor", "Ranking de Vendas por Forncedor", "Rank de Vendas por Fornecedor", "Classificação de Vendas por Fornecedor", "Lista de Vendas por Fornecedor", "Ranking de Vendas p/ Fornecedor", "Ranking de Venda por Fornecedor", "Rnking de Venda por Fornecedor", "Rankng de Venda por Fornecedor", "Ranking d Venda por Fornecedor", "Ranking de Venda po Fornecedor", "Ranking de Venda por Fornecdor", "Ranking de Venda por Forncedor", "Rank de Venda por Fornecedor", "Classificação de Venda por Fornecedor", "Lista de Venda por Fornecedor", "Ranking de Venda p/ Fornecedor"}
        ,
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
        {"Ranking de Vendas por Cliente", new Tuple<string[], string>(new[]
        {"Ranking de Vendas por Cliente", "Rnking de Vendas por Cliente", "Rankng de Vendas por Cliente", "Ranking d Vendas por Cliente", "Ranking de Vendas po Cliente", "Ranking de Vendas por Clinte", "Ranking de Vendas por Client", "Rank de Vendas por Cliente", "Classificação de Vendas por Cliente", "Lista de Vendas por Cliente", "Ranking de Vendas p/ Cliente", "Ranking de Venda por Clientes", "Rnking de Venda por Clientes", "Rankng de Venda por Clientes", "Ranking d Venda por Clientes", "Ranking de Venda po Clientes", "Ranking de Venda por Clentes", "Ranking de Venda por Clietes", "Rank de Venda por Clientes", "Classificação de Venda por Clientes", "Lista de Venda por Clientes", "Ranking de Venda p/ Clientes"}
        ,
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
        {"Ranking de Vendas por Grupo de Clientes", new Tuple<string[], string>(new[]
        {"Ranking de Vendas por Grupo de Cliente", "Rnking de Vendas por Grupo de Cliente", "Rankng de Vendas por Grupo de Cliente", "Ranking d Vendas por Grupo de Cliente", "Ranking de Vendas po Grupo de Cliente", "Ranking de Vendas por Grupo d Cliente", "Ranking de Vendas por Grupo de Client", "Ranking de Vendas por Grupo de Clente", "Rank de Vendas por Grupo de Cliente", "Classificação de Vendas por Grupo de Cliente", "Lista de Vendas por Grupo de Cliente", "Ranking de Vendas p/ Grupo de Cliente", "Ranking de Vendas por Grupos de Cliente", "Rnking de Vendas por Grupos de Cliente", "Rankng de Vendas por Grupos de Cliente", "Ranking d Vendas por Grupos de Cliente", "Ranking de Vendas po Grupos de Cliente", "Ranking de Vendas por Grupos d Cliente", "Ranking de Vendas por Grupos de Clente", "Ranking de Vendas por Grupos de Client", "Rank de Vendas por Grupos de Cliente", "Classificação de Vendas por Grupos de Cliente", "Lista de Vendas por Grupos de Cliente", "Ranking de Vendas p/ Grupos de Cliente", "Ranking de Vendas por Grupos de Clientes", "Rnking de Vendas por Grupos de Clientes", "Rankng de Vendas por Grupos de Clientes", "Ranking d Vendas por Grupos de Clientes", "Ranking de Vendas po Grupos de Clientes", "Ranking de Vendas por Grupos d Clientes", "Ranking de Vendas por Grupos de Clentes", "Ranking de Vendas por Grupos de Cientes", "Rank de Vendas por Grupos de Clientes", "Classificação de Vendas por Grupos de Clientes", "Lista de Vendas por Grupos de Clientes", "Ranking de Vendas p/ Grupos de Clientes"}
        , 
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

        {"Ranking de Vendas por Tipo de Consumidor", new Tuple<string[], string>(new[]


        {"Ranking de Vendas por Tipos de Consumidor", "Rnking de Vendas por Tipos de Consumidor", "Rankng de Vendas por Tipos de Consumidor", "Ranking d Vendas por Tipos de Consumidor", "Ranking de Vendas po Tipos de Consumidor", "Ranking de Vendas por Tipos d Consumidor", "Ranking de Vendas por Tipos de Consumidr", "Ranking de Vendas por Tipos de Consumido", "Rank de Vendas por Tipos de Consumidor", "Classificação de Vendas por Tipos de Consumidor", "Lista de Vendas por Tipos de Consumidor", "Ranking de Vendas p/ Tipos de Consumidor", "Ranking de Vendas por Tipo de Consumidor", "Rnking de Vendas por Tipo de Consumidor", "Rankng de Vendas por Tipo de Consumidor", "Ranking d Vendas por Tipo de Consumidor", "Ranking de Vendas po Tipo de Consumidor", "Ranking de Vendas por Tipo d Consumidor", "Ranking de Vendas por Tipo de Consumidr", "Ranking de Vendas por Tipo de Consumido", "Rank de Vendas por Tipo de Consumidor", "Classificação de Vendas por Tipo de Consumidor", "Lista de Vendas por Tipo de Consumidor", "Ranking de Vendas p/ Tipo de Consumidor", "Ranking de Vendas por Esfera", "Rnking de Vendas por Esfera", "Rankng de Vendas por Esfera", "Ranking d Vendas por Esfera", "Ranking de Vendas po Esfera", "Ranking de Vendas por Esfer", "Ranking de Vendas por Esfra", "Rank de Vendas por Esfera", "Classificação de Vendas por Esfera", "Lista de Vendas por Esfera", "Ranking de Vendas p/ Esfera", "Ranking de Vendas por Esferas", "Rnking de Vendas por Esferas", "Rankng de Vendas por Esferas", "Ranking d Vendas por Esferas", "Ranking de Vendas po Esferas", "Ranking de Vendas por Esfras", "Ranking de Vendas por Eseras", "Rank de Vendas por Esferas", "Classificação de Vendas por Esferas", "Lista de Vendas por Esferas", "Ranking de Vendas p/ Esferas"}
        , 
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
        {"Ranking de Vendas por Vendedor", new Tuple<string[], string>(new[]
                {"Ranking de Vendas por Tipos de Consumidor", "Rnking de Vendas por Tipos de Consumidor", "Rankng de Vendas por Tipos de Consumidor", "Ranking d Vendas por Tipos de Consumidor", "Ranking de Vendas po Tipos de Consumidor", "Ranking de Vendas por Tipos d Consumidor", "Ranking de Vendas por Tipos de Consumidr", "Ranking de Vendas por Tipos de Consumido", "Rank de Vendas por Tipos de Consumidor", "Classificação de Vendas por Tipos de Consumidor", "Lista de Vendas por Tipos de Consumidor", "Ranking de Vendas p/ Tipos de Consumidor", "Ranking de Vendas por Tipo de Consumidor", "Rnking de Vendas por Tipo de Consumidor", "Rankng de Vendas por Tipo de Consumidor", "Ranking d Vendas por Tipo de Consumidor", "Ranking de Vendas po Tipo de Consumidor", "Ranking de Vendas por Tipo d Consumidor", "Ranking de Vendas por Tipo de Consumidr", "Ranking de Vendas por Tipo de Consumido", "Rank de Vendas por Tipo de Consumidor", "Classificação de Vendas por Tipo de Consumidor", "Lista de Vendas por Tipo de Consumidor", "Ranking de Vendas p/ Tipo de Consumidor", "Ranking de Vendas por Esfera", "Rnking de Vendas por Esfera", "Rankng de Vendas por Esfera", "Ranking d Vendas por Esfera", "Ranking de Vendas po Esfera", "Ranking de Vendas por Esfer", "Ranking de Vendas por Esfra", "Rank de Vendas por Esfera", "Classificação de Vendas por Esfera", "Lista de Vendas por Esfera", "Ranking de Vendas p/ Esfera", "Ranking de Vendas por Esferas", "Rnking de Vendas por Esferas", "Rankng de Vendas por Esferas", "Ranking d Vendas por Esferas", "Ranking de Vendas po Esferas", "Ranking de Vendas por Esfras", "Ranking de Vendas por Eseras", "Rank de Vendas por Esferas", "Classificação de Vendas por Esferas", "Lista de Vendas por Esferas", "Ranking de Vendas p/ Esferas"}

        ,
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

        {"Estoque", new Tuple<string[], string>(new[] {"Estoque", "Estoqe", "Estoqu", "Estok", "Etoque", "Estque", "Estoc", "Stock", "Inventário", "Estoque ", "Estock", "Estoue", "Estoq", "Estocue", "Estoquue", "Esto"}, 
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
                    if (connection == null)
                        Console.WriteLine("Conexão inválida");

                     
                    Archive archive = new Archive();
                    archive.Id = 1;
                    archive.query = pair.Value.Item2;
                    var desc = pair.Key + " " + connection.description;
                    var descp = (desc.Length >= 25 ? desc.Substring(0,25) : desc);
                    archive.description = (string.IsNullOrEmpty(descp) ? "Gen" : descp)+DateTime.Now.ToString("hhmmss");
                    archive.format = "E";
                    archive.data = (await SchedulerApp_Query.ExecuteAsync(pair.Value.Item2,connection)).Item2;
                    archive.titleReport = "Solicitação Relatório "+pair.Key;
                    return new Tuple <Archive,SchedulerApp_Connection> (archive, connection); // Retorna a consulta SQL correspondente
                }
            }

            return null; // Se não encontrar nenhuma correspondência
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
        private static string GetDateInterpretation(string input)
        {
            var interpretation = new StringBuilder("Data -> ");

            input = input.ToLower();
            var periodIndex = Regex.Match(input, @"per[ií]odo d[eo]|entre").Index;
            if (periodIndex > 0)
            {
                var matchLength = Regex.Match(input, @"per[ií]odo d[eo]|entre").Length;
                input = input.Substring(periodIndex + matchLength);
            }

            var datePattern = @"(\d{1,2}[-/]\d{1,2}[-/]\d{4}|\d{4}[-/]\d{1,2}[-/]\d{1,2}|\d{1,2} de [a-z]+ de \d{4}|[a-z]+ de \d{4}|[a-z]+ desse ano|[a-z]+ do ano passado|[a-z]+ do ano retrasado|\d{1,2} dias atrás|primeiro [a-z]+ de \d{4}|[a-z]+ até [a-z]+|dia \d{1,2} de [a-z]+|dia \d{1,2} de [a-z]+ de \d{4}|\d{4}|[1-2] semestre|[1-4] trimestre)";
            var matches = Regex.Matches(input, datePattern);

            if (input.Contains("até hoje") || input.Contains("a partir de"))
            {
                interpretation.Append($"Período: de {matches[0].Value} até hoje");
            }
            else if (matches.Count == 1)
            {
                interpretation.Append($"Data única: {matches[0].Value}");
            }
            else if (matches.Count == 2)
            {
                interpretation.Append($"Período: de {matches[0].Value} até {matches[1].Value}");
            }
            else
            {
                interpretation.Append("Período: Este ano");
            }

            return interpretation.ToString();
        }


        public static List<string> getInterpretation(string input)
        {
            var interpretations = new List<string>();

            // Chama o método para interpretar informações sobre produtos
            var productInterpretation = GetProductInterpretation(input);
            if (!string.IsNullOrEmpty(productInterpretation))
            {
                interpretations.Add("Produto -> " + productInterpretation);
            }

            // Chama o método para interpretar informações sobre clientes
            var customerInterpretation = GetCustomerInterpretation(input);
            if (!string.IsNullOrEmpty(customerInterpretation))
            {
                interpretations.Add("Cliente -> " + customerInterpretation);
            }

            // Chama o método para interpretar o tipo de cliente
            var clientTypeInterpretation = GetClientTypeInterpretation(input);
            if (!string.IsNullOrEmpty(clientTypeInterpretation))
            {
                interpretations.Add("Tipo de Cliente -> " + clientTypeInterpretation);
            }

            // Chama o método para interpretar informações sobre fabricantes ou fornecedores
            var manufacturerInterpretation = GetManufacturerInterpretation(input);
            if (!string.IsNullOrEmpty(manufacturerInterpretation))
            {
                interpretations.Add("Fabricante/Fornecedor -> " + manufacturerInterpretation);
            }

            // Chama o método para interpretar informações sobre fabricantes ou fornecedores
            var stateInterpretation = GetStateInterpretation(input);
            if (!string.IsNullOrEmpty(manufacturerInterpretation))
            {
                interpretations.Add("Estado -> " + stateInterpretation);
            }

            // Chama o método para interpretar informações sobre datas
            var dateInterpretation = GetDateInterpretation(input);
            if (!string.IsNullOrEmpty(dateInterpretation))
            {
                interpretations.Add("Data -> " + dateInterpretation);
            }

            // Chama o método para interpretar informações sobre a conexão
            var connectionInterpretation = GetConnectionInterpretation(input);
            if (!string.IsNullOrEmpty(connectionInterpretation))
            {
                interpretations.Add("Conexão -> "+connectionInterpretation);
            }
            

            // Concatena todas as interpretações em uma única string
            return interpretations;
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
