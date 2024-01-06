import http from 'k6/http'
import { sleep } from 'k6'

export let options = {
    insecureSkipTLSVerify: true,
    noConnectionReuse: false,
    stages: [
        { duration: '2m', target: 100 },
        { duration: '5m', target: 100 },
        { duration: '2m', target: 200 },
        { duration: '5m', target: 200 },
        { duration: '2m', target: 300 },
        { duration: '5m', target: 300 },
        { duration: '2m', target: 400 },
        { duration: '4m', target: 400 },
        { duration: '10m', target: 0},
    ],
};

export default () => {
    const req_genPwd = {
        method: 'POST',
        url: '<BASE_URL>/gen-pwd',
        body: JSON.stringify({
            title: 'Title of Website',
            masterKey: '<MASTER KEY/PASSWORD>',
        }),
        params: {
            headers: {
                'Content-type': 'application/json',
            },
        }
    }
    const req_auth_genPwd = {
        method: 'POST',
        url: '<BASE_URL>/auth/gen-pwd',
        body: JSON.stringify({
            title: '<SITE_TITLE>',
        }),
        params: {
            headers: {
                'Content-type': 'application/json',
                'Authorization': 'Bearer <YOUR_ACCESS_TOKEN>'
            },
        }
    }

    const responses = http.batch([req_genPwd, req_auth_genPwd])
}