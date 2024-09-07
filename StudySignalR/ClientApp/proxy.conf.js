const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:17912';

const PROXY_CONFIG = [
  {
    context: [
      "/weatherforecast",
      "/chatHub",
      "/api"
   ],
    // proxyTimeout: 10000,
    target: target,
    secure: false,
    ws:true,
    logLevel: 'debug',
    // headers: {
    //   Connection: 'Keep-Alive'
    // }
  }
]

module.exports = PROXY_CONFIG;
