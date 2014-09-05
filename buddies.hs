mport System.Random (randomRIO)
import Control.Monad (liftM)
import Data.Maybe (maybeToList)

snipers :: [String]
snipers = ["Kieran", "Dean", "Matt", "Tats", "Brent", "Alex", "Ryan", "Ben"]

pairs :: [(String, String)]
pairs = [(x, y) | x <- snipers, y <- snipers, x /= y]

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

flatten :: [(a, a)] -> [a]
flatten ps = ps >>= (\(x, y) -> [x, y])

notBuddies :: (String, String) -> [(String, String)] -> Bool
notBuddies p acc = not $ isPaired (fst p) || isPaired (snd p)
		    where isPaired x = x `elem` (flatten acc)

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

buddies :: IO [(String, String)]
buddies = choose 4 pairs notBuddies
