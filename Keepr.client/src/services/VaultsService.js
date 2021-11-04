import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"


class VaultsService{

  async getVaultsByProfileId(profileId){

    const res = await api.get('api/profiles/'+ profileId + '/vaults')
    logger.log('get vaults by profileId', res)
    AppState.vaults = res.data
  }
  async getUserVaults(profileId){

    const res = await api.get('api/profiles/'+ profileId + '/vaults')
    logger.log('get vaults by profileId', res)
    AppState.userVaults = res.data
  }
  async getVaultById(vaultId){
    const res = await api.get('api/vaults/' + vaultId)
    logger.log('this is the chosen vault', res.data)
    AppState.vault = res.data
  }
  async postVault(vaultData){
    const res = await api.post('api/vaults', vaultData)
    logger.log('This is the posted Vault', res.data)
    AppState.vaults.push(res.data)
  }

  async deleteVault(vaultId){
    const res = await api.delete('api/vaults/' + vaultId)
    AppState.vaults = AppState.vaults.filter(v => v.id !== vaultId)
  }
}
export const vaultsService = new VaultsService()