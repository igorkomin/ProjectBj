using AutoMapper;
using ProjectBj.Entities;
using ProjectBj.ViewModels.History;
using System.Collections.Generic;

namespace ProjectBj.BusinessLogic.Helpers.ViewMapHelpers
{
    public static class HistoryMapHelper
    {
        public static IEnumerable<GetGameHistoryHistoryView> GetGameHistoryView(IEnumerable<History> history)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<History, GetGameHistoryHistoryView>());
            var gameHistoryViews = new List<GetGameHistoryHistoryView>();
            foreach (var entry in history)
            {
                GetGameHistoryHistoryView gameHistoryView = Mapper.Map<GetGameHistoryHistoryView>(entry);
                gameHistoryViews.Add(gameHistoryView); 
            }
            return gameHistoryViews;
        }

        public static IEnumerable<GetFullHistoryHistoryView> GetFullHistoryView(IEnumerable<History> history)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<History, GetFullHistoryHistoryView>());
            var fullHistoryViews = new List<GetFullHistoryHistoryView>();
            foreach (var entry in history)
            {
                GetFullHistoryHistoryView fullHistoryView = Mapper.Map<GetFullHistoryHistoryView>(entry);
                fullHistoryViews.Add(fullHistoryView);
            }
            return fullHistoryViews;
        }
    }
}
