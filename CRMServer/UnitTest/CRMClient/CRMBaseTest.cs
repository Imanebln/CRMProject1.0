using CRMClient;
using CRMServer.Models.CRM;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;

namespace UnitTest.CRMClient {
	[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
	public abstract class CRMBaseTest<T> where T : ICrmEntity {
		protected readonly CRMService crmService;
		protected readonly ITestOutputHelper output;
		protected T entity;
		protected CrudFunctions<T> CrudFunctions;

		public CRMBaseTest(CRMService crmService, ITestOutputHelper output) {
			this.crmService=crmService;
			this.output=output;
		}

		[Fact, Priority(1)]
		public void InsertTest() {
			T? inserted = CrudFunctions.Create(entity).Result;
			Assert.NotNull(inserted);
			T? retreived = CrudFunctions.Read(entity.GetUnique());
			Assert.Equal(retreived, inserted);
			T? existed = CrudFunctions.Create(entity).Result;
			Assert.Null(existed);
			Assert.NotNull(retreived);
			entity = retreived;
		}

		[Fact, Priority(2)]
		public void EditTest() {
			string oldUnique = entity.GetUnique();
			output.WriteLine(oldUnique);
			T? tmp = CrudFunctions.Read(oldUnique);
			Assert.NotNull(tmp);
			string? updatedField = SimpleUpdate(ref tmp);
			tmp = CrudFunctions.Update(tmp).Result;
			Assert.NotNull(tmp);
			Assert.Equal(updatedField, GetUpdatetField(tmp));
		}

		[Fact, Priority(3)]
		public void DeleteTest() {
			string oldUnique = entity.GetUnique();
			T? tmp = CrudFunctions.Read(oldUnique);
			Assert.NotNull(tmp);
			tmp = CrudFunctions.Delete(tmp).Result;
			Assert.NotNull(tmp);
			tmp = CrudFunctions.Read(oldUnique);
			Assert.Null(tmp);
		}
		protected abstract string? SimpleUpdate(ref T entityEdited);
		protected abstract string? GetUpdatetField(T entityEdited);
	}

	public class CrudFunctions<T> {
		public Func<T, Task<T?>> Create;
		public Func<string, T?> Read;
		public Func<T, Task<T?>> Update;
		public Func<T, Task<T?>> Delete;

		public CrudFunctions(Func<T, Task<T?>> create, Func<string, T?> read, Func<T, Task<T?>> update, Func<T, Task<T?>> delete) {
			Create=create;
			Read=read;
			Update=update;
			Delete=delete;
		}
	}
}
