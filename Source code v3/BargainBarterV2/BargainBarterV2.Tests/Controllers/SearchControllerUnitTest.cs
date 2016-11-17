﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using BargainBarterV2;
using BargainBarterV2.Controllers;
using System.Threading.Tasks;
using NUnit.Framework;
using BargainBarterV2.Models;
using NSubstitute;

namespace BargainBarterV2.Tests.Controllers
{
    [TestFixture]
    public class SearchControllerUnitTest
    {
        private SearchController _controller;
        private IUnitOfWork _unitOfWork;
        private IGenericRepository<BarterAdd> _barterAddRepo;

        [SetUp]
        public void Init()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _barterAddRepo = Substitute.For<IGenericRepository<BarterAdd>>();
            _unitOfWork.BarterAddRepository.Returns(_barterAddRepo);
            _controller = new SearchController(_unitOfWork);
        }

        [Test]
        public void CategorySearch_Calls_BarterAdsRepository()
        {
            string searchstring = "Hat";
            _controller.CategorySearch(searchstring);
            _unitOfWork.Received().BarterAddRepository.Get(a => a.Category == searchstring);
        }

        [Test]
        public void Search_Calls_BarterAdsRepository()
        {
            string searchstring = "Hat";
            _controller.Index(searchstring);
            _unitOfWork.Received().BarterAddRepository.Get(p=> p.Titel.Contains(searchstring) && p.Traded != true);
        }
        
    }
}