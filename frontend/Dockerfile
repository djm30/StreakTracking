FROM nginx

WORKDIR /app
RUN curl -fsSL https://deb.nodesource.com/setup_18.x | bash -
RUN apt-get install -y nodejs

COPY package*.json ./

RUN npm install

COPY . .

RUN npm run build

RUN rm -r /usr/share/nginx/html/*

COPY nginx.conf /etc/nginx/nginx.conf

RUN cp -a dist/. /usr/share/nginx/html