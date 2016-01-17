using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using StepAlarmPersistence.DataModel;
using System.Collections.ObjectModel;

namespace StepAlarmPersistence.Persistence
{
    public class DatabaseAccess
    {
        /// <summary>
        /// Databaseaccess member.
        /// </summary>
        SQLiteConnection db;

        /// <summary>
        /// sync lock.
        /// </summary>
        private static readonly object DbLock = new object();

        /// <summary>
        /// Constructor.
        /// </summary>
        public DatabaseAccess()
        {
            db = DatabaseHelper.getConnection();
            DatabaseHelper.Instance.InitializeDatabase();
        }

        /// <summary>
        /// Deletes Tables.
        /// </summary>
        public void ClearDataBaseAccess()
        {
            lock (DbLock)
            {
                db.DeleteAll<AlarmEntities>();
            }
        }

        /// <summary>
        /// Returns single Alarm Entity with alarm label as primary key.
        /// </summary>
        /// <param name="alarmLabel"></param>
        /// <returns></returns>
        public AlarmEntities ReadAlarmEntity(string alarmLabel)
        {
            lock(DbLock)
            {
                using (var db = new SQLiteConnection(DatabaseHelper.GetDBPath()))
                {
                    AlarmEntities entity = db.Query<AlarmEntities>("select * from AlarmEntities where AlarmLabel = " + alarmLabel).FirstOrDefault();
                    return entity;
                }
            }
        }

        /// <summary>
        /// Returns all the Alarm entities in db.
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<AlarmEntities> ReadAlarmEntities()
        {
            lock(DbLock)
            {
                using(var db = new SQLiteConnection(DatabaseHelper.GetDBPath()))
                {
                    List<AlarmEntities> entities = db.Table<AlarmEntities>().ToList<AlarmEntities>();
                    ObservableCollection<AlarmEntities> entitiesList = new ObservableCollection<AlarmEntities>(entities);
                    return entitiesList;
                }
            }
        }

        /// <summary>
        /// Updates Alarm entity in DB.
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateAlarmEntity(AlarmEntities entity)
        {
            lock(DbLock)
            {
                using (var db = new SQLiteConnection(DatabaseHelper.GetDBPath()))
                {
                    AlarmEntities existingAlarmEntity = db.Query<AlarmEntities>("select * from AlarmEntities where AlarmLabel =" + entity.AlarmLabel).FirstOrDefault();
                    if(existingAlarmEntity != null)
                    {
                        existingAlarmEntity.AlarmLabel = entity.AlarmLabel;
                        existingAlarmEntity.AlarmRepeat = entity.AlarmRepeat;
                        existingAlarmEntity.AlarmSound = entity.AlarmSound;
                        existingAlarmEntity.AlarmTime = entity.AlarmTime;
                        existingAlarmEntity.SnoozeTime = entity.SnoozeTime;
                        existingAlarmEntity.IsWalkUpAlarmEnabled = entity.IsWalkUpAlarmEnabled;
                        existingAlarmEntity.NoOfSteps = entity.NoOfSteps;
                        db.RunInTransaction(() =>
                            {
                                db.Update(existingAlarmEntity);
                            });
                    }
                }
            }
        }

        /// <summary>
        /// Inserts new Alarm entity in DB.
        /// </summary>
        /// <param name="entity"></param>
        public void InsertAlarmEntity(AlarmEntities entity)
        {
            lock(DbLock)
            {
                using (var db = new SQLiteConnection(DatabaseHelper.GetDBPath()))
                {
                    db.RunInTransaction(() =>
                    {
                        db.Insert(entity);
                    });
                }
            }
        }

        /// <summary>
        /// Deletes Alarm entity from DB.
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteAlarmEntity(AlarmEntities entity)
        {
            lock(DbLock)
            {
                using (var db = new SQLiteConnection(DatabaseHelper.GetDBPath()))
                {
                    AlarmEntities existingAlarmEntity = db.Query<AlarmEntities>("select * from AlarmEntities where AlarmLabel =" + entity.AlarmLabel).FirstOrDefault();
                    if(existingAlarmEntity != null)
                    {
                        db.RunInTransaction(() =>
                            {
                                db.Delete(existingAlarmEntity);
                            });
                    }
                }
            }
        }

        /// <summary>
        /// Delete the Alarm entity table.
        /// </summary>
        public void DeleteAllAlarmEntities()
        {
            lock(DbLock)
            {
                using (var db = new SQLiteConnection(DatabaseHelper.GetDBPath()))
                {
                    db.DropTable<AlarmEntities>();
                }
            }
        }
    }
}