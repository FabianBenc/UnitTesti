using AutoMapper;
using HoteliTest.DTO;
using HoteliTest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiTestiranje.TestiranjeApiKontrolera
{
    [TestClass]
    public class ApiStavkaRacunaTest
    {
        private static readonly HttpClient client = new HttpClient(new HttpClientHandler()
        { UseDefaultCredentials = true })
        { BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("ApiEnd")) };
        private static int testStavkaRacunaID;

        [ClassInitialize]
        public static void SetUpFixture(TestContext tc)
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<StavkaRacuna, StavkaRacunaDTO>().ReverseMap();
            });
            testStavkaRacunaID = GetStavkaRacunaID().Result;
        }

        [TestMethod]
        public void GetReturnsAccountItemWithSameID()
        {
            using (var response = client.GetAsync("api/StavkaRacuna/11").Result)
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "There doesnt exist a account item with this id");
            }
        }

        [TestMethod]
        public void GetAllAccountItems()
        {
            using (var response = client.GetAsync("api/StavkaRacuna/get").Result)
            {
                var responseResult =
                    JsonConvert.DeserializeObject<List<StavkaRacunaDTO>>(response.Content.ReadAsStringAsync().Result);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "There arent any account items");
            }
        }

        [TestMethod]
        public void GetServiceWithNonExistentId()
        {
            using (var response = client.GetAsync("api/AccountItem/getAccountItem/-1").Result)
            {
                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode, "A account item exists with this id");
            }
        }

        [TestMethod]
        public async Task PostAccountItem()
        {
            var accountItem = new StavkaRacunaDTO()
            {
                UslugaID = 1,
                Kolicina = 2,
                RacunID = 9
            };

            var content = new StringContent(JsonConvert.SerializeObject(accountItem), Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync("api/StavkaRacuna/", content))
            {
                var id = await response.Content.ReadAsStringAsync();
                int parseID = int.Parse(id);
                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode, "The account item has an invalid model state");
                await client.DeleteAsync($"api/AccountItem/DeleteAccountItem/{parseID}");
            }
        }

        [TestMethod]
        public async Task PostInvalidAccountItem()
        {
            var accountItem = new StavkaRacunaDTO();

            var content = new StringContent(JsonConvert.SerializeObject(accountItem), Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync("api/AccountItem/CreateAccountItem", content))
            {
                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode, "The account item has a valid model state");
            }
        }

        [TestMethod]
        public async Task UpdateAccountItem()
        {
            var accountItem = new StavkaRacunaDTO()
            {
                StavkaRacunaID = testStavkaRacunaID,
                UslugaID = 1,
                Kolicina = 10,
                RacunID = 9
            };

            var content = new StringContent(JsonConvert.SerializeObject(accountItem), Encoding.UTF8, "application/json");

            using (var response = await client.PutAsync($"api/AccountItem/UpdateAccountItem/{accountItem.StavkaRacunaID}", content))
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "The account item has an invalid model state, or the id you wanted to update is wrong");
            }
        }

        [TestMethod]
        public async Task UpdateAccountItemInvalidID()
        {
            var accountItem = new StavkaRacunaDTO();

            var content = new StringContent(JsonConvert.SerializeObject(accountItem), Encoding.UTF8, "application/json");

            using (var response = await client.PutAsync("api/Service/UpdateService/-1", content))
            {
                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode, "The account has an invalid model state, or the id you wanted to update is wrong");
            }
        }

        /*[TestMethod]
        public async Task DeleteAccountItem()
        {
            int id = await GetStavkaRacunaID();
            using (var response = await client.DeleteAsync($"api/AccountItem/DeleteAccountItem/{id}"))
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "There does not exist a account item with this ID");
            }
        }*/

        [TestMethod]
        public async Task DeleteNonExistingAccountItem()
        {
            using (var response = await client.DeleteAsync("api/AccountItem/DeleteAccountItem/-1"))
            {
                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode, "The account item exists");
            }
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            client.DeleteAsync($"api/AccountItem/DeleteAccountItem/{testStavkaRacunaID}");
        }

        private static async Task<int> GetStavkaRacunaID()
        {
            var accountItem = new StavkaRacunaDTO()
            {
                UslugaID = 1,
                Kolicina = 4,
                RacunID = 9
            };

            var content = new StringContent(JsonConvert.SerializeObject(accountItem), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/StavkaRacuna/Create", content);
            var id = await response.Content.ReadAsStringAsync();
            int parseID = int.Parse(id);
            //Int32.TryParse(id, out int parseID);

            return parseID;
        }
    }
}

