using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WalletService.Models;
using WalletServices.Data;
using WalletServices.Dtos;

namespace WalletService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<IActionResult> GetWallets() 
        {
            Console.WriteLine("--> Getting Wallet <--");
            var walletItem = await _repo.GetAllWallet();
            return Ok(walletItem);
        }
        [HttpPost]
        public async Task<IActionResult> CreateWallet(CreateWalletDto createWalletDto)
        {
            var walletModel = _mapper.Map<Wallet>(createWalletDto);
            var usernameWallet = _repo.GenerateId();
            walletModel.Username = usernameWallet;
            await _repo.Create(walletModel);
            _repo.SaveChanges();

            var readWallet = _mapper.Map<ReadWalletDto>(walletModel);
            return Ok(readWallet);
            // var walletModel = _mapper.Map<Wallet>(createWalletDto);
            // var usernameWallet = _repo.GenerateId();
            // walletModel.Username = usernameWallet;
            // walletModel.Cash = 0;
            // await _repo.Create(walletModel);
            // _repo.SaveChanges();

            // var readWallet = _mapper.Map<ReadWalletDto>(walletModel);
            // return Ok(readWallet);
        }
        [HttpPut("{username}")]
        public async Task<IActionResult> EditWallet(string username ,EditWalletDto editWalletDto)
        {
            try
            {
                var walletModel = _mapper.Map<Wallet>(editWalletDto);
                walletModel.Username = username;
                await _repo.Edit(username, walletModel);
                _repo.SaveChanges();

                var readWalletDto = _mapper.Map<ReadWalletDto>(walletModel);
                return Ok(readWalletDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("topUp")]
        public async Task<IActionResult> TopUpWallet(string username, int cash)
        {
            try
            {
                await _repo.Topup(cash, username);
                _repo.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("cashOut")]
        public async Task<IActionResult> CashOutWallet(string username, int cash)
        {
            try
            {
                await _repo.CashOut(cash, username);
                _repo.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}