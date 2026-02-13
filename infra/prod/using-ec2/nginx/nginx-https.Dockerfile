FROM node:24-alpine as builder

WORKDIR /app

COPY ./frontend .

RUN ls -al

RUN npm i pnpm -g

RUN pnpm i --config.confirmModulesPurge=false

ENV VITE_API_URL=/api

RUN pnpm build

FROM nginx:stable-alpine

COPY --from=builder /app/build/client /usr/share/nginx/html

EXPOSE 80 443
