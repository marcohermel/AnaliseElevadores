using System;
using Xunit;
using AnaliseElevadores.Service;
using AnaliseElevadores.Service.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnaliseElevadores.Service.Test
{
    public class ElevadorServiceTests
    {
        #region PROPS_AND_CONSTRUCTOR
        private ElevadorService _elevadorService;
        public ElevadorServiceTests()
        {
            List<int> andares = new List<int> { 1, 2, 3, 4, 5 };
            List<char> elevadores = new List<char> { 'A', 'B', 'C', 'D', 'E' };
            List<char> turnos = new List<char> { 'M', 'V', 'N' };
            List<RespostaDto> respostas = new List<RespostaDto> {
                new RespostaDto { Andar = 1, Elevador = 'A', Turno = 'M' },
                new RespostaDto { Andar = 2, Elevador = 'A', Turno = 'V' },
                new RespostaDto { Andar = 2, Elevador = 'C', Turno = 'M' },
                new RespostaDto { Andar = 2, Elevador = 'C', Turno = 'V' },
                new RespostaDto { Andar = 2, Elevador = 'C', Turno = 'V' },
                new RespostaDto { Andar = 2, Elevador = 'D', Turno = 'V' },
                new RespostaDto { Andar = 3, Elevador = 'D', Turno = 'V' },
                new RespostaDto { Andar = 3, Elevador = 'D', Turno = 'V' },
                new RespostaDto { Andar = 3, Elevador = 'E', Turno = 'V' },
            };

            _elevadorService = new ElevadorService(respostas, andares, elevadores, turnos);
        }
        #endregion

        #region TESTS
        [Fact(DisplayName = "andarMenosUtilizado Deve retornar uma List contendo o(s) andar(es) menos utilizado(s).")]
        public async void andarMenosUtilizadoTest()
        {
            //Arrange
            List<int> expectedList = new List<int> { 4, 5 };
            //Act
            List<int> actualList = await Task.FromResult(_elevadorService.andarMenosUtilizado());
            //Assert
            Assert.Equal(expectedList, actualList);
        }

        [Fact(DisplayName = "elevadorMaisFrequentado Deve retornar uma List contendo o(s) elevador(es) mais frequentado(s).")]
        public async void elevadorMaisFrequentado()
        {
            //Arrange
            List<char> expectedList = new List<char> { 'C', 'D' };
            //Act
            List<char> actualList = await Task.FromResult(_elevadorService.elevadorMaisFrequentado());
            //Assert
            Assert.Equal(expectedList, actualList);
        }

        [Fact(DisplayName = "periodoMaiorFluxoElevadorMaisFrequentado Deve retornar uma List contendo o período de maior fluxo de cada um dos elevadores mais frequentados (se houver mais de um).")]
        public async void periodoMaiorFluxoElevadorMaisFrequentado()
        {
            //Arrange
            List<char> expectedList = new List<char> { 'V' };
            //Act
            List<char> actualList = await Task.FromResult(_elevadorService.periodoMaiorFluxoElevadorMaisFrequentado());
            //Assert
            Assert.Equal(expectedList, actualList);
        }
        [Fact(DisplayName = "elevadorMenosFrequentado Deve retornar uma List contendo o(s) elevador(es) menos frequentado(s).")]
        public async void elevadorMenosFrequentado()
        {
            //Arrange
            List<char> expectedList = new List<char> { 'B' };
            //Act
            List<char> actualList = await Task.FromResult(_elevadorService.elevadorMenosFrequentado());
            //Assert
            Assert.Equal(expectedList, actualList);
        }
        [Fact(DisplayName = "periodoMenorFluxoElevadorMenosFrequentado Deve retornar uma List contendo o período de menor fluxo de cada um dos elevadores menos frequentados (se houver mais de um).")]
        public async void periodoMenorFluxoElevadorMenosFrequentado()
        {
            //Arrange
            List<char> expectedList = new List<char> { 'M', 'V', 'N' };
            //Act
            List<char> actualList = await Task.FromResult(_elevadorService.periodoMenorFluxoElevadorMenosFrequentado());
            //Assert
            Assert.Equal(expectedList, actualList);
        }
        [Fact(DisplayName = "periodoMaiorUtilizacaoConjuntoElevadores Deve retornar uma List contendo o(s) periodo(s) de maior utilização do conjunto de elevadores. ")]
        public async void periodoMaiorUtilizacaoConjuntoElevadores()
        {
            //Arrange
            List<char> expectedList = new List<char> { 'V' };
            //Act
            List<char> actualList = await Task.FromResult(_elevadorService.periodoMaiorUtilizacaoConjuntoElevadores());
            //Assert
            Assert.Equal(expectedList, actualList);
        }
        [Fact(DisplayName = "percentualDeUsoElevadorA Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador A em relação a todos os serviços prestados.")]
        public async void percentualDeUsoElevadorA()
        {
            //Arrange
            float expected = 22.22f;
            //Act
            float actual = await Task.FromResult(_elevadorService.percentualDeUsoElevadorA());
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact(DisplayName = "percentualDeUsoElevadorB  Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador B em relação a todos os serviços prestados.")]
        public async void percentualDeUsoElevadorB()
        {
            //Arrange
            float expected = 0f;
            //Act
            float actual = await Task.FromResult(_elevadorService.percentualDeUsoElevadorB());
            //Assert
            Assert.Equal(expected, actual);
        }
        [Fact(DisplayName = "percentualDeUsoElevadorC Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador C em relação a todos os serviços prestados.")]
        public async void percentualDeUsoElevadorC()
        {
            //Arrange
            float expected = 33.33f;
            //Act
            float actual = await Task.FromResult(_elevadorService.percentualDeUsoElevadorC());
            //Assert
            Assert.Equal(expected, actual);
        }
        [Fact(DisplayName = "percentualDeUsoElevadorD Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador D em relação a todos os serviços prestados.")]
        public async void percentualDeUsoElevadorD()
        {
            //Arrange
            float expected = 33.33f;
            //Act
            float actual = await Task.FromResult(_elevadorService.percentualDeUsoElevadorD());
            //Assert
            Assert.Equal(expected, actual);
        }
        [Fact(DisplayName = "percentualDeUsoElevadorE  Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador E em relação a todos os serviços prestados.")]
        public async void percentualDeUsoElevadorE()
        {
            //Arrange
            float expected = 11.11f;
            //Act
            float actual = await Task.FromResult(_elevadorService.percentualDeUsoElevadorE());
            //Assert
            Assert.Equal(expected, actual);
            
        }
        #endregion
    }
}
