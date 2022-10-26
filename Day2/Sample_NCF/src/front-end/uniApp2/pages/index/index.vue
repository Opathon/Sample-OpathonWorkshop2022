<template>
	<view class="content">
		<image class="logo" :src="userInfo.avatarUrl || '/static/logo.png'"></image>
		<view class="nickName">
			昵称:{{userInfo.nickName || 'uni-app'}}
		</view>
		<view class="text-area">
			<button class="bnt" @tap="getUserProfile">获取个人信息</button>
		</view>
	</view>
</template>

<script>
	export default {
		data() {
			return {
				userInfo:''
			}
		},
		onLoad() {
			
		},
		methods: {
			// 获取用户信息
			getUserProfile() {
				// 获取用户信息
				uni.getUserProfile({
					// 声明获取用户个人信息后的用途，后续会展示在弹窗中，请谨慎填写
					desc: '用于完善会员资料', 
					success: (res) => {
						console.log('getUserProfile res',res);
						console.log('用户信息', userInfo);
						const {userInfo} = res
						this.userInfo = userInfo
					}
				})
			},
			// 获取手机号
			getPhoneNumber(e) {
				console.log('getPhoneNumber e',e);
				if (!e.detail.iv) {
					uni.showToast({
						title: '获取手机号失败',
						icon: 'none'
					})
					return;
				}
				var that = this;
				// 验证code值是否过期
				uni.checkSession({
					success(val) {
						if (val.errMsg == 'checkSession:ok') {
							var obj = {
								code: that.code,
								iv: e.detail.iv,
								encryptedData: e.detail.encryptedData
							}
							that.decryptPhone(obj);
						} else {
							uni.login({
								provider: 'weixin',
								success(res) {
									let code = res.code;
									var obj = {
										code,
										iv: e.detail.iv,
										encryptedData: e.detail.encryptedData
									}
									that.decryptPhone(obj);
								}
							})
						}
					}
				})
			},
			// 调用接口 解密 获取手机号
			decryptPhone(obj) {
				var that = this;
				//传给后台解密，获得手机号
				uni.request({
					url: '',
					method: 'GET',
					data: obj,
					success: (cts) => {
						// 换取成功后 暂存这些数据 留作后续操作  
						console.log('cts',cts);
					},
					fail: (er) => {
						uni.showToast({
							title: '获取手机号失败，请重试',
							icon: 'none'
						})
					}
				})
			}
		}
	}
</script>

<style>
	.content {
		display: flex;
		flex-direction: column;
		align-items: center;
		justify-content: center;
	}

	.logo {
		height: 200rpx;
		width: 200rpx;
		margin-top: 200rpx;
		margin-left: auto;
		margin-right: auto;
		margin-bottom: 50rpx;
	}

	.text-area {
		display: flex;
		flex-direction: column;
		justify-content: center;
	}
	
	.nickName{
		text-align: center;
	}
	
	.bnt{
		margin-top: 15rpx;
	}

	.title {
		font-size: 36rpx;
		color: #8f8f94;
	}
</style>
