import http from 'k6/http'
import { sleep } from 'k6'

export let options = {
    insecureSkipTLSVerify: true,
    noConnectionReuse: false,
    vus: 1,
    duration: '1s',
};

export default () => {
    http.post(
        '<BASE_URL>/auth/gen-pwd',
        JSON.stringify({
            title: '<SITE_TITLE>',
        }),
        {
            headers: {
                'Content-type': 'application/json',
                'Authorization': 'Bearer <YOUR_ACCESS_TOKEN>'
            },
        }
    )

    http.post(
        '<BASE_URL>/gen-pwd',
        JSON.stringify({
            title: '<SITE_TITLE>',
            masterKey: '<MASTER_KEY/PASSWORD>',
        }),
        {
            headers: {
                'Content-type': 'application/json',
            },
        }
    )
}