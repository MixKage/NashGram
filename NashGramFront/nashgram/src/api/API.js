import axios from 'axios';

export const HTTP = axios.create({
    baseURL: `https://localhost:7113/`,
    headers: {
        'Content-Type': 'application/json',
    }
})