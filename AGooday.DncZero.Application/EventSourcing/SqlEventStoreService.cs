using AGooday.DncZero.Domain.Core.Events;
using AGooday.DncZero.Domain.Interfaces;
using AGooday.DncZero.Infrastructure.Repository.EventSourcing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Application.EventSourcing
{
    /// <summary>
    /// 事件存储服务类
    /// </summary>
    public class SqlEventStoreService : IEventStoreService
    {
        // 注入我们的仓储接口
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IUser _user;

        public SqlEventStoreService(IEventStoreRepository eventStoreRepository, IUser user)
        {
            _eventStoreRepository = eventStoreRepository;
            _user = user;
        }

        /// <summary>
        /// 保存事件模型统一方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="theEvent"></param>
        public void Save<T>(T theEvent) where T : Event
        {
            // 对事件模型序列化 
            // https://stackoverflow.com/questions/7397207/json-net-error-self-referencing-loop-detected-for-type/38382021
            // https://stackoverflow.com/questions/5769200/serialize-one-to-many-relationships-in-json-net
            var serializedData = JsonConvert.SerializeObject(theEvent, new JsonSerializerSettings()
            {
                //Formatting = Formatting.Indented,
                //PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            var storedEvent = new StoredEvent(
                theEvent,
                serializedData,
                _user.Name);

            _eventStoreRepository.Store(storedEvent);
        }
    }
}
