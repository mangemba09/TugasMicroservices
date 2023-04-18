using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WalletService.Data;
using WalletService.Dtos;
using WalletService.Models;

namespace WalletService.Controllers
{
    [ApiController]
    [Route("api/w/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletRepo _repo;
        private readonly IMapper _mapper;

        public WalletController(IWalletRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;

        }

        [HttpGet]
        public ActionResult<IEnumerable<ReadWalletDto>> GetAllProduct()
        {
            var walletItem = _repo.GetAllWallet();
            var walletReadDtoList = _mapper.Map<IEnumerable<ReadWalletDto>>(walletItem);
            return Ok(walletReadDtoList);
        }

        [HttpGet("{name}", Name = "GetByWalletName")]
        public async Task<ActionResult> GetByName(string name)
        {
            var wallet = await _repo.GetByName(name);
            var readWallet = _mapper.Map<ReadWalletDto>(wallet);
            return Ok(readWallet);
        }

        [HttpPut ("Topup")]
        public async Task<ActionResult> TopupWallet(TopupWalletDto topupWalletDto)
        {
            try
            {
                var wallet = _mapper.Map<Wallet>(topupWalletDto);
                wallet.UserName = topupWalletDto.UserName;
                await _repo.TopupWallet(wallet);
                _repo.SaveChanges();
                var returnWallet = await _repo.GetByName(topupWalletDto.UserName);
                var readProductDto = _mapper.Map<ReadWalletDto>(returnWallet);
                return Ok(readProductDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Pay")]
        public async Task<ActionResult> OrderWallet(OrderWalletDto orderWalletDto)
        {
             try
            {
                var wallet = _mapper.Map<Wallet>(orderWalletDto);
                wallet.UserName = orderWalletDto.UserName;
                await _repo.OrderWallet(wallet);
                _repo.SaveChanges();
                var returnWallet = await _repo.GetByName(orderWalletDto.UserName);
                var readProductDto = _mapper.Map<ReadWalletDto>(returnWallet);
                return Ok(readProductDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            // try
            // {
            //     var wallet = _mapper.Map<Wallet>(orderWalletDto);
            //     wallet.UserName = name;
            //     await _repo.OrderWallet(name, wallet);
            //     _repo.SaveChanges();
            //     var returnWallet = await _repo.GetByName(name);
            //     var readProductDto = _mapper.Map<ReadWalletDto>(returnWallet);
            //     return Ok(readProductDto);
            // }
            // catch (Exception ex)
            // {
            //     return BadRequest(ex.Message);
            // }
        }
    }
}