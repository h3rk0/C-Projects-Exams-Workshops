using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlClient;
using TeamBuilder.App.Core;
using TeamBuilder.Data;

namespace TeamBuilder.App
{
    class Application
    {
        static void Main(string[] args)
        {
			
			EnsureDatabaseIsCreated();

			Engine engine = new Engine(new Utilities.CommandInterpreter());
			engine.Run();
        }

		private static void EnsureDatabaseIsCreated()
		{
			// this method resets entities in database without delete-create 
			using (TeamBuilderContext context = new TeamBuilderContext())
			{
				ResetDatabase(context);
				context.SaveChanges();
				//context.Database.EnsureCreated();
			}
		}

		private static void ResetDatabase(TeamBuilderContext context, bool shouldDeleteDatabase = false)
		{
			if (shouldDeleteDatabase)
			{
				context.Database.EnsureDeleted();
				context.Database.EnsureCreated();
			}

			context.Database.EnsureCreated();

			var disableIntegrityChecksQuery = "EXEC sp_MSforeachtable @command1='ALTER TABLE ? NOCHECK CONSTRAINT ALL'";
			context.Database.ExecuteSqlCommand(disableIntegrityChecksQuery);

			var deleteRowsQuery = "EXEC sp_MSforeachtable @command1='DELETE FROM ?'";
			context.Database.ExecuteSqlCommand(deleteRowsQuery);


			var enableIntegrityChecksQuery = "EXEC sp_MSforeachtable @command1='ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL'";
			context.Database.ExecuteSqlCommand(enableIntegrityChecksQuery);

			var reseedQuery = "EXEC sp_MSforeachtable @command1='DBCC CHECKIDENT(''?'', RESEED, 0)'";
			try
			{
				context.Database.ExecuteSqlCommand(reseedQuery);
			}
			catch (SqlException)
			{
			}
		}
	}
}
