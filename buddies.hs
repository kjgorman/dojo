module Buddies (buddies) where

import System.Environment (getArgs)
import System.Random (randomRIO)
import Control.Monad (liftM, join, (>=>))
import Data.List (intersect)
import Data.Maybe (maybeToList)

pairs :: [String] -> [[String]]
pairs people = [[x, y] | x <- people, y <- people, x /= y]

pick :: [a] -> IO (Maybe a)
pick [] = return Nothing
pick xs = liftM (Just . (xs !!)) $ randomRIO (lo, hi)
          where lo = 0
 	   	hi = length xs - 1

pickWhere :: [a] -> (a -> Bool) -> IO (Maybe a)
pickWhere xs pred = do
    maybeChoice <- pick xs
    return $ do
        x <- maybeChoice
	if pred x then Just x else Nothing

notBuddies :: [String] -> [[String]] -> Bool
notBuddies p = null . intersect p . join

choose :: Eq a => Int -> [a] -> (a -> [a] -> Bool) -> IO [a]
choose 0 _ _ = return []
choose n s p = choose' n s p []

choose' :: Eq a => Int -> [a] -> (a -> [a] -> Bool) -> [a] -> IO [a]
choose' 0 _ _ acc   = return acc
choose' n src pred acc = do
    choice <- pickWhere src (\p -> pred p acc)
    case choice of
        Nothing -> choose' n src pred acc
    	(Just p) -> choose' (n-1) src pred (p:acc)

oddOneOut :: [String] -> ([String], Maybe String)
oddOneOut ts = if length ts `mod` 2 /= 0
               then (tail ts, Just $ head ts)
               else (ts, Nothing)

allocateLoner :: String -> [[String]] -> [[String]]
allocateLoner s [] = [[s]]
allocateLoner s (x:xs) = (s:x):xs

buddies :: [String] -> IO [[String]]
buddies s = let (people, loner) = oddOneOut s
                assigned = choose (length people `div` 2) (pairs people) notBuddies
            in case loner of
              Nothing -> assigned
              Just s -> liftM (allocateLoner s) assigned

main :: IO ()
main = getArgs >>= (buddies >=> print)
