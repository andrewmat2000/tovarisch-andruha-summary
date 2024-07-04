FROM node:20.12.2-alpine3.19 as build

WORKDIR /src

COPY frontend .

RUN npm i

RUN npm run build

FROM nginx:1.27.0 as run

COPY --from=build /src/build /app

RUN chown nginx /app