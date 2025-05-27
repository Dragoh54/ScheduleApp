#!/bin/bash

echo "Waiting for mongo1 to become available..."

#user     - admin
#password - admin
until mongosh --host mongo1 -u admin -p admin --eval 'db.runCommand({ ping: 1 })' >/dev/null 2>&1; do
  sleep 1
  echo "Still waiting for mongo1..."
done

echo "Initializing replica set..."

#user     - admin
#password - admin
mongosh --host mongo1 -u admin -p admin -f /scripts/init-replica.js

if [ $? -eq 0 ]; then
  echo "Replica set initialized successfully"
  exit 0
else
  echo "Failed to initialize replica set"
  exit 1
fi