using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using AnaliseElevadores.Service.Dtos;
using AnaliseElevadores.Service;

namespace AnaliseElevadores
{
    class Program
    {
        static void Main(string[] args)
        {

            using (StreamReader r = new StreamReader("input.json"))
            {
                var json = r.ReadToEnd();
                
                List<RespostaDto> respostas = JsonConvert.DeserializeObject<List<RespostaDto>>(json);

                List<int> andares = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
                List<char> elevadores = new List<char> { 'A', 'B', 'C', 'D', 'E' };
                List<char> turnos = new List<char> { 'M', 'V', 'N' };

                ElevadorService elevadoresService = new ElevadorService(respostas, andares, elevadores, turnos);

                List<int> andarMenosUtilizado = elevadoresService.andarMenosUtilizado();
                List<char> elevadorMaisFrequentado = elevadoresService.elevadorMaisFrequentado();
                List<char> elevadorMenosFrequentado = elevadoresService.elevadorMenosFrequentado();
                List<char> periodoMaiorFluxoElevadorMaisFrequentado = elevadoresService.periodoMaiorFluxoElevadorMaisFrequentado();
                List<char> periodoMaiorUtilizacaoConjuntoElevadores = elevadoresService.periodoMaiorUtilizacaoConjuntoElevadores();
                List<char> periodoMenorFluxoElevadorMenosFrequentado = elevadoresService.periodoMenorFluxoElevadorMenosFrequentado();
                float percentualDeUsoElevadorA = elevadoresService.percentualDeUsoElevadorA();
                float percentualDeUsoElevadorB = elevadoresService.percentualDeUsoElevadorB();
                float percentualDeUsoElevadorC = elevadoresService.percentualDeUsoElevadorC();
                float percentualDeUsoElevadorD = elevadoresService.percentualDeUsoElevadorD();
                float percentualDeUsoElevadorE = elevadoresService.percentualDeUsoElevadorE();

                Console.WriteLine("Resultados:");
                Console.WriteLine("Andar(es) Menos Utilizado(s): {0}", andarMenosUtilizado.ToStringItens());
                Console.WriteLine("Elevador(es) Mais Frequentado(s): {0}", elevadorMaisFrequentado.ToStringItens());
                Console.WriteLine("Elevador(es) Menos Frequentado(s): {0}", elevadorMenosFrequentado.ToStringItens());
                Console.WriteLine("Periodo(s) de Maior Fluxo do(s) Elevador(es) Mais Frequentado(s): {0}", periodoMaiorFluxoElevadorMaisFrequentado.ToStringItens());
                Console.WriteLine("Periodo(s) de Maior Utilização do Conjunto de Elevadores: {0}", periodoMaiorUtilizacaoConjuntoElevadores.ToStringItens());
                Console.WriteLine("Periodo(s) de Menor Fluxo do(s) Elevador(es) Menos Frequentado(s): {0}", periodoMenorFluxoElevadorMenosFrequentado.ToStringItens());
                Console.WriteLine("Percentual de Uso do Elevador A: {0}", percentualDeUsoElevadorA);
                Console.WriteLine("Percentual de Uso do Elevador B: {0}", percentualDeUsoElevadorB);
                Console.WriteLine("Percentual de Uso do Elevador C: {0}", percentualDeUsoElevadorC);
                Console.WriteLine("Percentual de Uso do Elevador D: {0}", percentualDeUsoElevadorD);
                Console.WriteLine("Percentual de Uso do Elevador E: {0}", percentualDeUsoElevadorE);
                Console.ReadLine();
            }
        }
    }
}
