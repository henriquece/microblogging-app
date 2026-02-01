#!/bin/bash
cd /social-media/infra/prod/using-ec2/docker-compose/

sudo docker compose up server-http

sudo docker compose run --rm certbot -v certonly --webroot --webroot-path /var/www/certbot/ -d api.henriquece.dev

sudo docker compose down

sudo docker compose up server-https