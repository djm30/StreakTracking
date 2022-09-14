FROM nginx

WORKDIR /app

ENV NODE_VERSION=18.6.0
RUN apt install -y curl
RUN curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v0.39.0/install.sh | bash
ENV NVM_DIR=/root/.nvm
RUN . "$NVM_DIR/nvm.sh" && nvm install ${NODE_VERSION}
RUN . "$NVM_DIR/nvm.sh" && nvm use v${NODE_VERSION}
RUN . "$NVM_DIR/nvm.sh" && nvm alias default v${NODE_VERSION}
ENV PATH="/root/.nvm/versions/node/v${NODE_VERSION}/bin/:${PATH}"
RUN node --version
RUN npm --version

COPY package*.json ./

RUN npm install

COPY . .

RUN npm run build

RUN rm -r /usr/share/nginx/html/*

COPY nginx.conf /etc/nginx/nginx.conf

RUN cp -a dist/. /usr/share/nginx/html