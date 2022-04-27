import Vue from 'vue'
import Vuex from 'vuex'
import user from '@/store/modules/user'
import address from '@/store/modules/address'
import bussinessdetails from '@/store/modules/businessDetailsMaster' 

Vue.use(Vuex)
const store = new Vuex.Store({
  modules: { 
    user,
    address,
    bussinessdetails
  }
})
export default store