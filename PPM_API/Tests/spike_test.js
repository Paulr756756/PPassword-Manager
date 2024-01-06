import http from 'k6/http'
import { sleep } from 'k6'

export let options = {
    insecureSkipTLSVerify: true,
    noConnectionReuse: false,
    stages: [
        { duration: '10s', target: 100 },
        { duration: '1m', target: 100 },
        { duration: '10s', target: 1400 },
        { duration: '3m', target: 1400 },
        { duration: '10s', target: 100 },
        { duration: '3m', target: 100 },
        { duration: '2m', target: 400 },
        { duration: '10s', target: 0 },
    ],
};

export default () => {
    const req_genPwd = {
        method: 'POST',
        url: '<BASE_URL>/gen-pwd',
        body: JSON.stringify({
            title: '<SITE_TITLE>',
            masterKey: '<MASTER_KEY/PASSWORD>',
        }),
        params: {
            headers: {
                'Content-type': 'application/json',
            },
        },
    };
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
        },
    };

    const responses = http.batch([req_genPwd, req_auth_genPwd])
}