import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  account: {},
  keeps: [],
  keep: {},
  vaults: [],
  vault: {},
  profile: {},
  vaultKeeps: [],
  vaultKeep: {},
  profileKeeps: [],
  userVaults: []
})
