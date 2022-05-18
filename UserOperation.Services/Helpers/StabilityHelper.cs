﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserOperation.Data.Entities;
using UserOperation.Data.Repository;
using UserOperation.Services.Dtos;

namespace UserOperation.Services.Helpers
{
   
    public class StabilityHelper : IStabilityHelper
    { 
        private IGenericRepository<Level> _levelRepository;
        private IGenericRepository<Project> _projectRepository;
        private IGenericRepository<StabilityLevel> _stabilityLevelRepository;
        private IGenericRepository<Criticality> _criticalityRepository;
        private IGenericRepository<Position> _positionRepository;
        private readonly IMapper _mapper;


        public StabilityHelper(IGenericRepository<Level> levelRepository, IGenericRepository<Project> projectRepository,
             IGenericRepository<StabilityLevel> stabilityLevelRepository,
             IGenericRepository<Criticality> criticalityRepository,
             IGenericRepository<Position> positionRepository, IMapper mapper)    
        {
            _levelRepository = levelRepository;
            _projectRepository = projectRepository;
            _stabilityLevelRepository = stabilityLevelRepository;
            _criticalityRepository = criticalityRepository;
            _positionRepository = positionRepository;
            _mapper = mapper;

        }
        
        public ICollection<LevelDto> GetLevels()
        {
            return _mapper.Map<List<LevelDto>>(_levelRepository.GetAll());
        }
        public ICollection<ProjectDto> GetProjects()
        {
            return _mapper.Map<List<ProjectDto>>(_projectRepository.GetAll());
        }
        public ICollection<StabilityLevelDto> GetStabilityLevels()
        {
            return _mapper.Map<List<StabilityLevelDto>>(_stabilityLevelRepository.GetAll()) ;
        }
        public ICollection<CriticalityDto> GetCriticalities()
        {
            return _mapper.Map<List<CriticalityDto>>(_criticalityRepository.GetAll());
        }
        public ICollection<PositionDto> GetPositions()
        {
            return _mapper.Map<List<PositionDto>>(_positionRepository.GetAll());
        }
    }
}