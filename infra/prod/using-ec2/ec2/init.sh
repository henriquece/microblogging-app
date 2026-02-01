#!/bin/bash
curl -fsSL https://get.docker.com -o get-docker.sh

sudo sh ./get-docker.sh

git clone https://github.com/henriquece/social-media.git

cd social-media/infra/prod/using-ec2/docker-compose/

sudo docker compose up server-http

sudo docker compose run --rm certbot -v certonly --webroot --webroot-path /var/www/certbot/ -d api.henriquece.dev