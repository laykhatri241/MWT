import { VuexModule, Module, Mutation, Action } from "vuex-module-decorators";
import callApi from "@/api/callApi";
import Product from "@/interfaces/product";
import Category from "@/interfaces/category";

@Module({ namespaced: true })
class Products extends VuexModule {
  products = new Product();
  categories = new Category();

  @Action
  public async AddProduct(data: Product): Promise<any> {
    data.SellerID = Number(localStorage.getItem("UserID"));
    return callApi
      .AsyncPOST("Product/AddProduct", data)
      .then((response) => {
        console.log("console", response);

        return response;
      })
      .catch((err) => {
        return err;
      });
  }
  @Action
  public async UpdateProduct(data: any): Promise<any> {
    data.SellerID = Number(localStorage.getItem("UserID"));
    return callApi
      .AsyncPOST("Product/UpdateProduct", data)
      .then((response) => {
        console.log(response);

        return response;
      })
      .catch((err) => {
        return err;
      });
  }
  @Action
  public async DeleteProduct(id: any): Promise<any> {
    return callApi
      .AsyncGET(`Product/DeleteProduct/${id}`)
      .then((response) => {
        console.log(response);

        return response;
      })
      .catch((err) => {
        return err;
      });
  }
  @Action
  public async Getcategory(data: any): Promise<any> {
    return callApi
      .AsyncPOST("Product/GetCategories", data)
      .then((response) => {
        // console.log(response);
        //localStorage.getItem('Token')
        return response;
      })
      .catch((err) => {
        return err;
      });
  }
  @Action
  public async GetAllProducts(data: any): Promise<any> {
    return callApi
      .AsyncGET("Product/GetMyProducts/" + localStorage.getItem("UserID"))
      .then((response) => {
        // console.log(response);

        return response;
      })
      .catch((err) => {
        return err;
      });
  }
  @Action
  public async GetProducts(id: any): Promise<any> {
    return callApi
      .AsyncGET(`Product/GetProduct/${id}` )
      .then((response) => {
        // console.log(response);

        return response;
      })
      .catch((err) => {
        return err;
      });
  }
}

export default Products;
