using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Snt22Progress.Contracts.Models.Questions;
using Snt22Progress.DataAccess.Models;

namespace Snt22Progress.BussinesLogic.Mappers.ConfigurationProfiles
{
	public class QuestionProfile : Profile
	{
		public QuestionProfile()
		{
			CreateMap<QuestionView, QuestionFullDto>()
				.ForMember(x => x._choise_ids, opt => opt.Ignore()) // Это служебные поля, их не надо маппить
				.ForMember(x => x._choise_texts, opt => opt.Ignore()) // Это служебные поля, их не надо маппить
				.ForMember(x => x._choise_votes_counts, opt => opt.Ignore()); // Это служебные поля, их не надо маппить

			CreateMap<QuestionFullDto, QuestionDto>()
				.ForMember(x => x.Creator, opt => opt.MapFrom(m => MapperHelper.InitCreator(m.creator_id, m.creator_fname, m.creator_lname, m.creator_mname)))
				.ForMember(x => x.Editor, opt => opt.MapFrom(m => MapperHelper.InitEditor(m.editor_id, m.editor_fname, m.editor_lname, m.editor_mname)))
				.ForMember(x => x.Choises, opt => opt.MapFrom(m => GetChoiseDtos(m)));

			CreateMap<QuestionCreateDto, Question>();

			CreateMap<ChoiseCreateDto, Choise>();
		}

		private static ChoiseDto[] GetChoiseDtos(QuestionFullDto src)
		{
			var choises = new List<ChoiseDto>();

			for (int i = 0; i < src._choise_ids.Length; i++)
			{
				var ch = new ChoiseDto
				{
					Id = src._choise_ids[i]
				};
				if (src._choise_texts != null && src._choise_texts.Length > i)
				{
					ch.Text = src._choise_texts[i];
				}
				if (src._choise_votes_counts != null && src._choise_votes_counts.Length > i)
				{
					ch.VotesCount = src._choise_votes_counts[i];
				}
				choises.Add(ch);
			}

			return choises.ToArray();
		}
	}
}
