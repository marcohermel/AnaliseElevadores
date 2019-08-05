using System;
using System.Collections.Generic;
using System.Linq;
using AnaliseElevadores.Service.Dtos;

namespace AnaliseElevadores.Service
{
    public class ElevadorService : IElevadorService
    {

        #region PROPS_AND_CONSTRUCTOR
        private List<RespostaDto> _respostas { get; set; }
        private List<int> _andares { get; set; }
        private List<char> _elevadores { get; set; }
        private List<char> _turnos { get; set; }

        public ElevadorService(List<RespostaDto> respostas, List<int> andares, List<char> elevadores, List<char> turnos)
        {
            _respostas = respostas;
            _andares = andares;
            _elevadores = elevadores;
            _turnos = turnos;
        }
        #endregion

        #region PUBLIC_FUNCTIONS
        public List<int> andarMenosUtilizado()
        {
            //agrupa as respostas por andares
            var groupListByFloor = _respostas.GroupBy(r => r.Andar)
              .Select(group => new { Andar = group.Key, Count = group.Count() });

            //completa com todos os andares (até os não respondidos)
            var answersByFloor = _andares.GroupJoin(groupListByFloor,
                a => a,
                g => g.Andar,
                (a, gs) => new { Andar = a, gs = gs.DefaultIfEmpty(new { Andar = a, Count = 0 }) })
                .SelectMany(s => s.gs.Select(g => new { s.Andar, g.Count }));

            //filtra conforme a função pede
            var filteredFloors = answersByFloor.Where(ra => ra.Count == answersByFloor.Min(r => r.Count))
                    .Select(s => s.Andar).ToList();

            return filteredFloors;

        }
        public List<char> elevadorMaisFrequentado()
        {
            var groupListByElevator = _respostas.GroupBy(r => r.Elevador)
              .Select(group => new { Elevador = group.Key, Count = group.Count() });

            var answersByElevator = _elevadores.GroupJoin(groupListByElevator,
              e => e,
              g => g.Elevador,
              (a, gs) => new { Elevador = a, gs = gs.DefaultIfEmpty(new { Elevador = a, Count = 0 }) })
              .SelectMany(s => s.gs.Select(g => new { s.Elevador, g.Count }));

            var filteredElevators = answersByElevator.Where(s => s.Count == answersByElevator.Max(g => g.Count))
                    .Select(s => s.Elevador).ToList();

            return filteredElevators;
        }
        public List<char> periodoMaiorFluxoElevadorMaisFrequentado()
        {
            List<char> elevadorMaisFrequentados = elevadorMaisFrequentado();

            var groupList = _respostas.Where(r => elevadorMaisFrequentados.Contains(r.Elevador))
              .GroupBy(r => r.Turno)
             .Select(group => new { Turno = group.Key, Count = group.Count() });

            var answersByPeriod = _turnos.GroupJoin(groupList,
              e => e,
              g => g.Turno,
              (a, gs) => new { Turno = a, gs = gs.DefaultIfEmpty(new { Turno = a, Count = 0 }) })
              .SelectMany(s => s.gs.Select(g => new { s.Turno, g.Count }));

            var filteredTurns = answersByPeriod.Where(s => s.Count == answersByPeriod.Max(g => g.Count))
                    .Select(s => s.Turno).ToList();

            return filteredTurns;

        }
        public List<char> elevadorMenosFrequentado()
        {
            var groupList = _respostas.GroupBy(r => r.Elevador)
               .Select(group => new { Elevador = group.Key, Count = group.Count() });

            var answersByElevator = _elevadores.GroupJoin(groupList,
              e => e,
              g => g.Elevador,
              (a, gs) => new { Elevador = a, gs = gs.DefaultIfEmpty(new { Elevador = a, Count = 0 }) })
              .SelectMany(s => s.gs.Select(g => new { s.Elevador, g.Count }));

            var filteredElevators = answersByElevator.Where(s => s.Count == answersByElevator.Min(g => g.Count))
                    .Select(s => s.Elevador).ToList();

            return filteredElevators;
        }
        public List<char> periodoMaiorUtilizacaoConjuntoElevadores()
        {

            var groupListByTurn = _respostas.GroupBy(r => r.Turno)
             .Select(group => new { Turno = group.Key, Count = group.Count() });

            var answersByPeriod = _turnos.GroupJoin(groupListByTurn,
              e => e,
              g => g.Turno,
              (a, gs) => new { Turno = a, gs = gs.DefaultIfEmpty(new { Turno = a, Count = 0 }) })
              .SelectMany(s => s.gs.Select(g => new { s.Turno, g.Count }));

            var filteredTurns = answersByPeriod.Where(s => s.Count == answersByPeriod.Max(g => g.Count))
                    .Select(s => s.Turno).ToList();

            return filteredTurns;
        }

        public List<char> periodoMenorFluxoElevadorMenosFrequentado()
        {
            List<char> elevadoresMenosFrequentados = elevadorMenosFrequentado();

            var groupListByTurn = _respostas.Where(r => elevadoresMenosFrequentados.Contains(r.Elevador))
              .GroupBy(r => r.Turno)
             .Select(group => new { Turno = group.Key, Count = group.Count() });

            var answersByPeriod = _turnos.GroupJoin(groupListByTurn,
              e => e,
              g => g.Turno,
              (a, gs) => new { Turno = a, gs = gs.DefaultIfEmpty(new { Turno = a, Count = 0 }) })
              .SelectMany(s => s.gs.Select(g => new { s.Turno, g.Count }));

            var filteredTurns = answersByPeriod.Where(s => s.Count == answersByPeriod.Min(g => g.Count))
                    .Select(s => s.Turno).ToList();

            return filteredTurns;
        }
        public float percentualDeUsoElevadorA() => percentElevator('A');
        public float percentualDeUsoElevadorB() => percentElevator('B');
        public float percentualDeUsoElevadorC() => percentElevator('C');
        public float percentualDeUsoElevadorD() => percentElevator('D');
        public float percentualDeUsoElevadorE() => percentElevator('E');
        #endregion

        #region PRIVATE_FUNCTIONS
        private int getCountAnswersByElevator(char elevator) => _respostas.Where(r => r.Elevador == elevator).Count();
        private float percentage(float count, float countTotal) => (count / countTotal) * 100;
        private float roundFloat(float value, int decimalPlaces = 2) => (float)Math.Round((double)value, decimalPlaces, MidpointRounding.AwayFromZero);
        private float percentElevator(char elevator) => roundFloat(percentage(getCountAnswersByElevator(elevator), _respostas.Count));
        #endregion

    }
}

