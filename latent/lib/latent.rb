module Latent

  dir = File.join(File.dirname(__FILE__))
  $LOAD_PATH.unshift File.join(dir,"latent")
  Dir.glob(File.join(dir, 'latent', '**', '*rb')).each {|f| require f }

  class UseCases
    class << self
      def rgb
        Importers.new.make_importer Importers.RGB
      end

      def cmyk
        Importers.new.make_importer Importers.CMYK
      end
    end
  end

end
