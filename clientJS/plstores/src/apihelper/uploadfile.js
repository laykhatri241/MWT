import http from "@/apihelper/api";
class UploadFilesService {

  upload(file) {
    let formData = new FormData();
    formData.append("file",file);
    AsyncPOST("Account/UploadAvatar", formData

      
    ).then(data=>{console.log(data); return data});
  }
  getFiles() {
    return http.get("/files");
  }
}
export default new UploadFilesService();