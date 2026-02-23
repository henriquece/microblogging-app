import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import axios from "axios";
import { useState } from "react";
import { Link } from "react-router";

const bobUserId = 1;

export const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
});

export default function Component() {
  const queryClient = useQueryClient();

  const { data } = useQuery({
    queryKey: ["posts"],
    queryFn: async () => {
      const response = await api.get("/posts");

      return response.data;
    },
  });

  const postMutation = useMutation({
    mutationFn: (newTodo: { newPostValue: string }) => {
      return api.post("/posts", {
        content: newTodo.newPostValue,
        userId: bobUserId,
      });
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["posts"] });
    },
  });

  const postDeleteMutation = useMutation({
    mutationFn: ({ postId }: { postId: number }) => {
      return api.delete(`/posts/${postId}`);
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["posts"] });
    },
  });

  const postCommentMutation = useMutation({
    mutationFn: ({ postId, comment }: { postId: number; comment: string }) => {
      return api.post(`/posts/${postId}/comment`, {
        userId: bobUserId,
        content: comment,
      });
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["posts"] });
    },
  });

  const postUnlikeMutation = useMutation({
    mutationFn: (postId: number) => {
      return api.delete(`/posts/${postId}/like`, {
        data: {
          userId: bobUserId,
        },
      });
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["posts"] });
    },
  });

  const postLikeMutation = useMutation({
    mutationFn: (postId: number) => {
      return api.post(`/posts/${postId}/like`, {
        userId: bobUserId,
      });
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["posts"] });
    },
  });

  const postCommentDeleteMutation = useMutation({
    mutationFn: ({
      postId,
      commentId,
    }: {
      postId: number;
      commentId: number;
    }) => {
      return api.delete(`/posts/${postId}/comment`, {
        data: {
          id: commentId,
        },
      });
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["posts"] });
    },
  });

  const postCommentLikeMutation = useMutation({
    mutationFn: ({
      postId,
      commentId,
    }: {
      postId: number;
      commentId: number;
    }) => {
      return api.post(`/posts/${postId}/comment/${commentId}/like`, {
        userId: bobUserId,
      });
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["posts"] });
    },
  });

  const postUnlikeCommentMutation = useMutation({
    mutationFn: ({
      postId,
      commentId,
      commentLikeId,
    }: {
      postId: number;
      commentId: number;
      commentLikeId: number;
    }) => {
      return api.delete(`/posts/${postId}/comment/${commentId}/like`, {
        data: {
          id: commentLikeId,
        },
      });
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["posts"] });
    },
  });

  const [newPostValue, setNewPostValue] = useState("");

  const [newPostCommentValue, setNewPostCommentValue] = useState("");

  return (
    <div className="flex-1 flex flex-col items-center gap-16 min-h-0">
      <p>Home</p>
      <Link to="/">Indexx</Link>
      <div>
        <p style={{ marginTop: 48 }}>New Post</p>
        <textarea
          style={{ width: 600, height: 50 }}
          value={newPostValue}
          onChange={(event) => setNewPostValue(event.target.value)}
        />
        <button
          style={{ display: "block" }}
          onClick={() => {
            postMutation.mutate({ newPostValue });
          }}
        >
          Send post
        </button>
      </div>
      <p style={{ marginTop: 48 }}>Posts</p>
      <ul>
        {data?.map(
          (post: {
            content: string;
            id: number;
            userId: number;
            userName: string;
            likes: { userId: number; userName: string }[];
            comments: {
              content: string;
              id: number;
              userId: number;
              userName: string;
              likes: { id: number; userId: number }[];
            }[];
          }) => {
            const hasOwnPostLike = post.likes.some(
              (like) => like.userId === bobUserId,
            );

            return (
              <li key={post.id}>
                <div
                  style={{
                    width: "600px",
                    height: "80px",
                    marginTop: "16px",
                    border: "1px solid black",
                  }}
                >
                  <p>{post.content}</p>
                  <div style={{ display: "flex" }}>
                    <span>{post.userName}</span>
                    <button
                      style={{ marginLeft: 120 }}
                      onClick={() => {
                        hasOwnPostLike
                          ? postUnlikeMutation.mutate(post.id)
                          : postLikeMutation.mutate(post.id);
                      }}
                    >
                      {hasOwnPostLike ? "Remove like" : "Like"}
                    </button>
                    <span style={{ marginLeft: 20 }}>
                      Likes: {post.likes.length}
                    </span>
                    <button
                      style={{ marginLeft: 120 }}
                      onClick={() => {
                        postDeleteMutation.mutate({ postId: post.id });
                      }}
                    >
                      Delete post
                    </button>
                  </div>
                </div>
                <div>
                  <div
                    style={{
                      width: "490px",
                      marginTop: "16px",
                      marginLeft: "100px",
                    }}
                  >
                    <textarea
                      style={{ width: "100%", height: 50 }}
                      value={newPostCommentValue}
                      onChange={(event) =>
                        setNewPostCommentValue(event.target.value)
                      }
                    />
                    <button
                      style={{ display: "block" }}
                      onClick={() => {
                        postCommentMutation.mutate({
                          postId: post.id,
                          comment: newPostCommentValue,
                        });
                      }}
                    >
                      Send comment
                    </button>
                  </div>
                  {post.comments.map((comment) => {
                    const hasOwnCommentLike = comment.likes.some(
                      (like) => like.userId === bobUserId,
                    );

                    return (
                      <p
                        key={comment.id}
                        style={{
                          width: "490px",
                          height: "40px",
                          marginTop: "16px",
                          marginLeft: "100px",
                          border: "1px solid black",
                        }}
                      >
                        {comment.content} - {comment.userName}
                        <button
                          style={{ marginLeft: 60 }}
                          onClick={() => {
                            hasOwnCommentLike
                              ? postUnlikeCommentMutation.mutate({
                                  postId: post.id,
                                  commentId: comment.id,
                                  commentLikeId: comment.likes.find(
                                    (like) => like.userId === bobUserId,
                                  )?.id as number,
                                })
                              : postCommentLikeMutation.mutate({
                                  postId: post.id,
                                  commentId: comment.id,
                                });
                          }}
                        >
                          {hasOwnCommentLike ? "Remove like" : "Like"}
                        </button>
                        <span style={{ marginLeft: 20 }}>
                          Likes: {comment.likes.length}
                        </span>
                        <button
                          style={{ marginLeft: 80 }}
                          onClick={() => {
                            postCommentDeleteMutation.mutate({
                              postId: post.id,
                              commentId: comment.id,
                            });
                          }}
                        >
                          Delete comment
                        </button>
                      </p>
                    );
                  })}
                </div>
              </li>
            );
          },
        )}
      </ul>
    </div>
  );
}
