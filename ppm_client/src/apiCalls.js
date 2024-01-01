import * as cts from "./constants"

export const getPasswordFromAPI = async (body) => {
    const response = await fetch(`${cts.baseUrl}${cts.genNoAuth}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(body)
    })

    if (!response.ok) throw new Error(`HTTP Error! Status ${response.status}`)
    return response.json()
}

export const auth_getPasswordFromAPI = async (body) => {
    const response = await fetch(`${cts.baseUrl}${cts.genAuth}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${localStorage.getItem('accessToken')}`
        },
        body: JSON.stringify(body)
    })

    if (!response.ok) throw new Error(`HTTP Error! Status ${response.status}`)
    return response.json()
}

export const getSavedSitesFromApi = async () => {
    const response = await fetch(`${cts.baseUrl}${cts.getSavedSites}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${localStorage.getItem('accessToken')}`
        }
    })

    if (!response.ok) throw new Error(`HTTP ERROR! Status ${response.status}`)
    return response.json()
}