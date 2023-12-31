import { ref } from 'vue'
import { baseUrl } from './constants'

const isAuthenticated = ref(false)

/**
 * Provides authentication-related functionality.
 * @returns {AuthHook} The authentication hook.
 */
export default function useAuth() {
    /**
     * User-login function.
     * @param {LoginRequest} model
     */
    const login = async (model) => {
        /**
         * @type {LoginResponse}
         */
        const response = await fetch(`${baseUrl}login`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(model)
        })
        //FIXME(Mechanism to check when current access token expired then refresh automatically)
        if (!response.ok) throw new Error(`HTTP Error! Status: ${response.status}`)

        const responseData = await response.json()
        localStorage.clear()
        localStorage.setItem('accessToken', responseData.accessToken)
        localStorage.setItem('refreshToken', responseData.refreshToken)
        localStorage.setItem('expireDate', (parseInt(responseData.expiresIn)*1000) + Date.now())

        isAuthenticated.value = true
    }

    /**
     * User-logout function
     */
    const logout = () => {
        isAuthenticated.value = false
        localStorage.clear()
    }

    // Gets a new access token from the API after previous has expired.
    const refreshLogin = async ()  => {
        const refreshToken = localStorage.getItem('refreshToken')
        const response = await fetch(`${baseUrl}refresh`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ 'refreshToken': refreshToken })
        })
        console.log(JSON.stringify({ 'refreshToken': refreshToken }),response)
        if (!response.ok) throw new Error(`HTTP Error! Status: ${response.status}`)
        
        const responseData = await response.json()
        localStorage.clear()
        localStorage.setItem('accessToken', responseData.accessToken)
        localStorage.setItem('refreshToken', responseData.refreshToken)
        localStorage.setItem('expireDate', (parseInt(responseData.expiresIn)*1000) + Date.now())
        
        isAuthenticated.value = true
    }

    return {
        isAuthenticated,
        refreshLogin,
        login,
        logout
    }
}