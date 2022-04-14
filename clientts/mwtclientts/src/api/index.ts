import axios from 'axios'
import store from '@/store'

const config = {
    baseURL: 'https://localhost:44301/MWT',
    headers: { Accept: 'application/json' }
  }
const call = axios.create(config)

export default call