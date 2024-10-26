using Backend.DTOs;
using Backend.Models;
using Backend.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class BeerService : ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>
    {
        //private StoreContext _context;
        private IRepository<Beer> _beerRepository;

        public BeerService(IRepository<Beer> beerRepository)
        {
            //_context = context;
            _beerRepository = beerRepository;
        }
        public async Task<IEnumerable<BeerDto>> Get()
        {
            var beers = await _beerRepository.Get();

            return beers.Select(b => new BeerDto
            {
                Id = b.BeerID,
                Name = b.Name,
                BrandID = b.BrandID,
                Alcohol = b.Alcohol
            });
        }
           
    
        public async Task<BeerDto> GetById(int id)
        {
            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                var beerDto = new BeerDto
                {
                    Id = beer.BeerID,
                    Name = beer.Name,
                    BrandID = beer.BrandID,
                    Alcohol = beer.Alcohol
                };

                return beerDto;
            }

            return null;
        }
        public async Task<BeerDto> Add(BeerInsertDto beerInsertDto)
        {
            var beer = new Beer()
            {
                Name = beerInsertDto.Name,
                BrandID = beerInsertDto.BrandID,
                Alcohol = beerInsertDto.Alcohol
            };

            await _beerRepository.Add(beer); // Todavia no inserta la data en la BD
            await _beerRepository.Save(); // Hasta que no insertamos esta linea no va a haber ningun cambio en la DB  
                                               // Al momento que insertamos el elemento en la base de dato ya tendra un id que se lo otorga AddAsync

            var beerDto = new BeerDto
            {
                Id = beer.BeerID,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandID = beer.BrandID,
            };

            return beerDto;
        }
        public async Task<BeerDto> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                beer.Name = beerUpdateDto.Name;
                beer.Alcohol = beerUpdateDto.Alcohol;
                beer.BrandID = beerUpdateDto.BrandID;

                _beerRepository.Update(beer);
                await _beerRepository.Save();   

                var beerDto = new BeerDto
                {
                    Id = beer.BeerID,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandID = beer.BrandID,
                };

                return beerDto;
            }

            return null;
        }
        public async Task<BeerDto> Delete(int id)
        {
            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                var beerDto = new BeerDto
                {
                    Id = beer.BeerID,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandID = beer.BrandID,
                };

                _beerRepository.Delete(beer);
                await _beerRepository.Save();

               
               return beerDto;
            }
             return null;
        }

    }
}
