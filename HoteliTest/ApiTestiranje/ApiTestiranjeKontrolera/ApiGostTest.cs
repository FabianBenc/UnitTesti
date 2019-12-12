using AutoMapper;
using HoteliTest.DAL;
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

namespace ApiTestiranje.ApiTestiranjeKontrolera
{
    [TestClass]
    public class ApiGostTest
    {
        private static readonly HttpClient client = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true }) { BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("ApiEnd")) };
        HotelContext db = new HotelContext();
        private static int testGuestId;

        [ClassInitialize]
        public static void SetUpFixture(TestContext tc)
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Gost, GostDTO>().ReverseMap();
            });
            testGuestId = CreateTestGuestId().Result;
        }

        [TestMethod]
        public void GetReturnsGuestWithSameId()
        {
            using (var response = client.GetAsync("api/Gost/1").Result)
            {
                Assert.IsNotNull(response, "response is null");
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "There doesnt exist a guest with this id");
            }
        }

        [TestMethod]
        public void GetAllGuests()
        {
            using (var response = client.GetAsync("api/Gost/").Result)
            {
                var responseResult =
                    JsonConvert.DeserializeObject<List<GostDTO>>(response.Content.ReadAsStringAsync().Result);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "There aren't any guests");
            }
        }

        [TestMethod]
        public void GetGuestWithNonExistentId()
        {
            using (var response = client.GetAsync("api/Gost/-1").Result)
            {
                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode, "A guest exists with this id");
            }
        }

        [TestMethod]
        public async Task PostGuests()
        {
            var guest = new GostDTO()
            {
                Ime = "Guesz1",
                Prezime = "Guser",
                Adresa = "AA",
                Email = "BB"
            };

            var content = new StringContent(JsonConvert.SerializeObject(guest), Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync("api/Gost/Create", content))
            {
                var id = await response.Content.ReadAsStringAsync();
                int parseID = int.Parse(id);
                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode, "The guest has an invalid model state");
                await client.DeleteAsync($"api/Gost/Delete/{parseID}");
            }
        }

        [TestMethod]
        public async Task PostInvalidGuest()
        {
            var guest = new GostDTO() //Last name je required zato prolazi ovaj test
            {
                Ime = "Guesz1",
            };

            var content = new StringContent(JsonConvert.SerializeObject(guest), Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync("api/Gost/Create", content))
            {
                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode, "The guest has a valid model state");
            }
        }

        [TestMethod]
        public async Task UpdateGuest()
        {
            var guest = new GostDTO()
            {
                GostID = testGuestId,
                Ime = "Guessadasz1",
                Prezime = "Guser",
                Adresa = "AA",
                Email = "BB"
            };

            var content = new StringContent(JsonConvert.SerializeObject(guest), Encoding.UTF8, "application/json");

            using (var response = await client.PutAsync($"api/Gost/Edit/{guest.GostID}", content))
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "The guest has an invalid model state , or the id you wanted to update is wrong");
            }
        }

        [TestMethod]
        public async Task UpdateGuestInvalidID()
        {
            var guest = new GostDTO()
            {
                GostID = -1,
                Ime = "Guessadasz1",
                Prezime = "Guser",
                Adresa = "AA",
                Email = "BB"
            };

            var content = new StringContent(JsonConvert.SerializeObject(guest), Encoding.UTF8, "application/json");

            using (var response = await client.PutAsync("api/Gost/Edit/-1", content))
            {
                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode, "The guest has an invalid model state, or the id you wanted to update is wrong");
            }
        }

        [TestMethod]
        public async Task DeleteGuest()
        {
            int id = await CreateTestGuestId();
            using (var response = await client.DeleteAsync($"api/Gost/Delete/{id}"))
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "There does not exist a guest with this ID");
            }
        }

        [TestMethod]
        public async Task DeleteNonExistingGuest()
        {
            using (var response = await client.DeleteAsync("api/Gost/Delete/-1"))
            {
                Assert.IsNotNull(response, "Response is null");
                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode, "The guest exists");
            }
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            client.DeleteAsync($"api/Gost/Delete/{testGuestId}");
        }

        private static async Task<int> CreateTestGuestId()
        {
            var guest = new GostDTO()
            {
                Ime = "PutDelete",
                Prezime = "Put",
                Adresa = "AA",
                Email = "BB"
            };

            var content = new StringContent(JsonConvert.SerializeObject(guest), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/Gost/Create", content);
            var id = await response.Content.ReadAsStringAsync();
            int parseID = int.Parse(id);

            return parseID;
        }
    }
}
