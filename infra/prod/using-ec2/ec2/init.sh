#!/bin/bash
cd /microblogging-app/infra/prod/using-ec2/docker-compose/

sudo docker compose up -d server-http

sudo docker compose run --rm certbot -v certonly --webroot --webroot-path /var/www/certbot/ -d microblogging-app.henriquece.dev

sudo docker compose down

sudo docker compose up server-https