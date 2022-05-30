using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserOperation.Data.Entities
{
    public class DataSeeder
    {
        private readonly ApplicationDbContext _dbContext;
        public DataSeeder(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (!_dbContext.Criticalities.Any())
            {
                var criticalities = new List<Criticality>()
                {
                    new Criticality()
                    {
                        CriticalityName = "Critical"
                    },
                    new Criticality()
                    {
                        CriticalityName = "Highcritical"
                    },
                    new Criticality()
                    {
                        CriticalityName = "Noncritical"
                    }

                };
                _dbContext.Criticalities.AddRange(criticalities);
                _dbContext.SaveChanges();
            }

            if (!_dbContext.Levels.Any())
            {
                var levels = new List<Level>()
                {
                    new Level()
                    {
                        LevelName = "L0"
                    },
                    new Level()
                    {
                        LevelName = "L1"
                    },
                    new Level()
                    {
                        LevelName = "L2"
                    }

                };
                _dbContext.Levels.AddRange(levels);
                _dbContext.SaveChanges();
            }
            
            if (!_dbContext.Positions.Any())
            {
                var positions = new List<Position>()
                {
                    new Position()
                    {
                        PositionName = "Software Developer"
                    },
                    new Position()
                    {
                        PositionName = "Senior Software Developer"
                    },
                    new Position()
                    {
                        PositionName = "QA Manual"
                    },
                    new Position()
                    {
                        PositionName = "Junior UX Designer"
                    },
                    new Position()
                    {
                        PositionName = "Junior Software Developer"
                    }

                };
                _dbContext.Positions.AddRange(positions);
                _dbContext.SaveChanges();
            }
            
            if (!_dbContext.Projects.Any())
            {
                var projects = new List<Project>()
                {
                    new Project()
                    {
                        ProjectName = "Flowbird"
                    },
                    new Project()
                    {
                        ProjectName = "Porsche"
                    },
                    new Project()
                    {
                        ProjectName = "Westfield"
                    },
                    new Project()
                    {
                        ProjectName = "Benenden"
                    },
                    new Project()
                    {
                        ProjectName = "Tivo"
                    }

                };
                _dbContext.Projects.AddRange(projects);
                _dbContext.SaveChanges();
            }
            
            if (!_dbContext.Reasons.Any())
            {
                var reasons = new List<Reason>()
                {
                    new Reason()
                    {
                        ReasonName = "Compensation"
                    },
                    new Reason()
                    {
                        ReasonName = "Project related"
                    },
                    new Reason()
                    {
                        ReasonName = "Relocation"
                    },
                    new Reason()
                    {
                        ReasonName = "Personal issues"
                    }

                };
                _dbContext.Reasons.AddRange(reasons);
                _dbContext.SaveChanges();
            }
            
            if (!_dbContext.StabilityLevels.Any())
            {
                var sLevels = new List<StabilityLevel>()
                {
                    new StabilityLevel()
                    {
                        StabilityLevelName = "Certain"
                    },
                    new StabilityLevel()
                    {
                        StabilityLevelName = "Moderately certain"
                    },
                    new StabilityLevel()
                    {
                        StabilityLevelName = "Uncertain"
                    }

                };
                _dbContext.StabilityLevels.AddRange(sLevels);
                _dbContext.SaveChanges();
            }


        }


        
    }
}
