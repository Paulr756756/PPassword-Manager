/**
 * User register request object
 * @typedef {object} RegisterRequest
 * @property {string} userName - The username of the user
 * @property {string} email - The email address of the user
 * @property {string} password - The password of the user
 */

/**
 * User login request object
 * @typedef {object} LoginRequest
 * @property {string} email - The email address of the user
 * @property {string} password - The password of the user
 */

/**
 * Custom hook for managing authentication-related functionality.
 * @typedef {object} AuthHook
 * @property {Ref<boolean>} isAuthenticated - Indicates whether the user is authenticated.
 * @property {function} login - Function to perform user login.
 * @property {function} logout - Function to perform user logout.
 * @property {function} refreshLogin - Function to refresh the user's login status.
 */

/**
 * @typedef {object} LoginResponse
 * @property {string} accessToken - Access Token to access auth endpoints.
 * @property {string} refreshToken - Refresh Token to get new access token when expired.
 * @property {string} expiresIn - Time in milliseconds until the access token expires.
 */