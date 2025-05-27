FROM mongo:6.0

COPY MONGO_REPLICA/keyfile /etc/mongo-keyfile
COPY MONGO_REPLICA/init-replica.js /scripts/init-replica.js
COPY MONGO_REPLICA/init-replica.sh /scripts/init-replica.sh

RUN chmod 400 /etc/mongo-keyfile && chown mongodb:mongodb /etc/mongo-keyfile