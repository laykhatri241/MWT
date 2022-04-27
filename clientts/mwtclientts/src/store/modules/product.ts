import { VuexModule, Module, Mutation, Action } from "vuex-module-decorators";
import callApi from "@/api/callApi";
import Product from "@/interfaces/product";
import Category from "@/interfaces/category";

@Module({ namespaced: true })
class Products extends VuexModule {
    products = new Product();
    categories = new Category();


    
}

export default Products;
