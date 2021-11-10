import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"


class VaultKeepsService{

  async getKeepsByVaultId(vaultId){
    const res = await api.get('api/vaults/'+ vaultId + '/keeps')
    logger.log('get keeps by vault id', res)
    AppState.vaultKeeps = res.data
  }

  async postVaultKeep(body){
  
    const res = await api.post('api/vaultkeeps', body)
    logger.log('post vaultkeep', res)
    AppState.vaultKeeps = res.data
  }
  async removeVaultKeep(vaultKeepId){
   
    const res = await api.delete('api/vaultkeeps/' + vaultKeepId)
    logger.log("this vault keep was deleted", res.data)
    AppState.vaultKeeps = AppState.vaultKeeps.filter(vk => vk.id !== vaultKeepId)
  }
  // async updateVaultKeep(vaultKeepId, vaultKeep){
  //   vaultKeep.keeps = vaultKeep.keeps +1
  //   const res = await api.
  // }
}
export const vaultKeepsService = new VaultKeepsService()