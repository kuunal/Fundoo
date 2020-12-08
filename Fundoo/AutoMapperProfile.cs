using AutoMapper;
using ModelLayer;
using ModelLayer.DTOs.AccountDto;
using ModelLayer.DTOs.NoteDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundoo
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Account, AccountResponseDto>();
            CreateMap<AccountRequestDto, Account> ();

            CreateMap<Note, NoteResponseDto>();
            CreateMap<NoteRequestDto, Note>();
        }
    }
}
