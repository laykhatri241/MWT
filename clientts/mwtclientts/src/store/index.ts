import Vue from 'vue'
import Vuex from 'vuex'
import user from '@/store/modules/user'
import address from '@/store/modules/address'
import bussinessdetails from '@/store/modules/businessDetailsMaster' 
import product from "@/store/modules/product"

Vue.use(Vuex)
const store = new Vuex.Store({
  modules: { 
    user,
    address,
    bussinessdetails,
    product
  }
})
export default store