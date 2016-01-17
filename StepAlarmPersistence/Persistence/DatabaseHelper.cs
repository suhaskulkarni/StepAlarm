using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Windows.Storage;
using System.IO;
using StepCounterWrapper.Common;
using StepAlarmPersistence.DataModel;

namespace StepAlarmPersistence.Persistence
{
    public class DatabaseHelper
    {
        /// <summary>
        /// Database Path.
        /// </summary>
        private static string dbPath;

        /// <summary>
        /// Database SQLite connection.
        /// </summary>
        private static SQLiteConnection db;

        /// <summary>
        /// DatabaseHelper instance.
        /// </summary>
        private static volatile DatabaseHelper instance;

        /// <summary>
        /// Singleton Instance.
        /// </summary>
        public static DatabaseHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DatabaseHelper();
                }
                return instance;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        private DatabaseHelper()
        {
            SetupDatabase();
        }

        /// <summary>
        /// Gets db connection by setting up Db.
        /// </summary>
        /// <returns></returns>
        public static SQLiteConnection getConnection()
        {
            if (instance == null)
            {
                SetupDatabase();
            }
            return db;
        }

        /// <summary>
        /// Sets up the db.
        /// </summary>
        private static void SetupDatabase()
        {
            dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, Constants.DBFileName);
            db = new SQLiteConnection(dbPath);
        }

        /// <summary>
        /// Creates db tables.
        /// </summary>
        public void InitializeDatabase()
        {
            db.CreateTable<AlarmEntities>();
        }

        /// <summary>
        /// Checks if db exists.
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public async Task<bool> DoesDbExistAsync(string dbName)
        {
            bool dbexist = true;
            try
            {
                StorageFile storageFile = await ApplicationData.Current.LocalFolder.GetFileAsync(dbName);
            }
            catch
            {
                dbexist = false;
            }

            return dbexist;
        }

        /// <summary>
        /// Drops db connection.
        /// </summary>
        public static void Close()
        {
            if (db != null)
                db.Close();
            instance = null;
        }

        /// <summary>
        /// Closes db connection and deletes database.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DeleteDatabase()
        {
            try
            {
                Close();
                if (await DoesDbExistAsync(Constants.DBFileName))
                {
                    StorageFile storageFile = await ApplicationData.Current.LocalFolder.GetFileAsync(Constants.DBFileName);
                    await storageFile.DeleteAsync();
                }
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }


        /// <summary>
        /// Returns DB path.
        /// </summary>
        /// <returns></returns>
        public static string GetDBPath()
        {
            return dbPath;
        }
    }
}