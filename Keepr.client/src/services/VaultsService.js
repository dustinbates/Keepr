import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { api } from "./AxiosService"

class VaultsService {

  async getVaultById(vaultId){
    const res = await api.get('api/vaults/' + vaultId)
    logger.log('[Got Vault]', res.data)
    AppState.activeVault = res.data
  }

  async getProfileVaults(id){
    const res = await api.get(`api/profiles/${id}/vaults`)
    logger.log('[GOT VAULTS]', res.data)
    AppState.profileVaults = res.data
  }

  async getVaultKeeps(vaultId){
    const res = await api.get(`api/vaults/${vaultId}/keeps`)
    logger.log('[Got VaultKeeps]', res.data)
    AppState.vaultKeeps = res.data
  }
  async createVault(vaultData){
    const res = await api.post('api/vaults', vaultData)
    Pop.toast("Created Vault!", 'success')
    AppState.vaults.push(res.data)
  }

  async deleteVault(vaultId){
    const res = await api.delete(`api/vaults/${vaultId}`)
    Pop.toast("Deleted Vault!", 'success')
    let deleteIndex = AppState.vaults.findIndex(v => v.id == vaultId)
    AppState.vaults.splice(deleteIndex, 1)
  }
}

export const vaultsService = new VaultsService()