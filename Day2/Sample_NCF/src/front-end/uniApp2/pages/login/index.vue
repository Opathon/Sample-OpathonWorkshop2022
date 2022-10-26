<template>
	<view class="content">
		<image class="logo" src="/static/logo.png"></image>
		<view class="text-area">
			<text class="title">{{title}}</text>
		</view>
		<!-- 登录 -->
		<button  @tap="login">登录</button>
	</view>
</template>

<script>
	export default {
		data() {
			return {
				title: '登录'
			}
		},
		onLoad(options) {

		},
		methods: {
			login(){
				uni.login({
					provider: 'weixin',
					success(res) {
						let code = res.code;
						
						that.decryptPhone(obj);
					}
				})
			},
			// 调起登录接口
			loginRequest(){
				uni.request({
					url:'',
					method:"GET",
					success: (res) => {
						// 存储
						// uni.setStorageSync('token')
						// 获取
						// uni.getStorageInfoSync('token')
					},
					fail: (err) => {
						
					},
					complete: () => {
						
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
		/* justify-content: center; */
	}

	.title {
		font-size: 36rpx;
		text-align: center;
		color: #8f8f94;
	}
</style>
