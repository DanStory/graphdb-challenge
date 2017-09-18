FROM node:6.11
RUN mkdir /app
WORKDIR /app
EXPOSE 80
ENV PORT=80
ENTRYPOINT ["npm", "start"]
COPY . .
RUN npm install